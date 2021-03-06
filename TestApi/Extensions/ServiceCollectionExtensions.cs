using BusinessLayer.Constraints;
using BusinessLayer.Managers;
using DataAccessLayer;
using DataAccessLayer.Classes;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.Infrastructure;

namespace TestApi.Extensions
{
	internal static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDefaultContext(this IServiceCollection services,
		                                                   IConfiguration          configuration)
		{
			var connectionString = configuration.GetConnectionString("Default");

			return services.AddDbContext<DefaultContext>(options =>
					options.UseNpgsql(connectionString, m => m.MigrationsAssembly("DataAccessLayer")));
		}

		public static IServiceCollection AddManagers(this IServiceCollection services)
		{
			return services.AddScoped<UserManager>()
			               .AddScoped<ConstraintManager>()
			               .AddScoped<TaskManager>()
			               .AddScoped<ColumnManager>()
			               .AddScoped<BoardManager>()
			               .AddScoped<HistoryManager>();
		}

		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			return services.AddScoped<IRepository<Task>, TaskRepository>()
			               .AddScoped<IRepository<Column>, ColumnRepository>()
			               .AddScoped<IRepository<Board>, BoardRepository>();
		}

		public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
		{
			var origin = configuration["ApplicationHostOrigin"];

			var policy = new CorsPolicyBuilder()
			             .WithOrigins(origin)
			             .AllowAnyHeader()
			             .AllowAnyMethod()
			             .Build();

			services.AddCors(builder => builder.AddPolicy("DefaultPolicy", policy));

			return services;
		}

	    public static IServiceCollection AddConstraints(this IServiceCollection services)
	    {
	        services.AddScoped<LockedTaskConstraint>();
	        services.AddScoped<ConstraintCollection>();
            return services;
	    }

	    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	    {
	        services.AddScoped(typeof(CommandFactory<>));
	        services.AddScoped(typeof(CommandValidationFilter<,>));
            return services;
	    }



    }
}