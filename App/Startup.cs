﻿using App.Extensions;
using App.Services.Classes;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace App
{
	public class Startup : StartupBase
	{
		public IConfiguration      Configuration { get; }
		public IHostingEnvironment Environment   { get; }

		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Configuration = configuration;
			Environment   = environment;
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddDefaultContext(Configuration);
            services.AddSingleton<IDbInitializerService,TestDbInitializerService>();
			services.AddMvc();
		}

		public override void Configure(IApplicationBuilder app)
		{
			app.EnvironmentDependentConfiguration(Environment);

			app.UseStaticFiles();

			app.UseMvc(router =>
			{
				router.MapRoute
				(
						"index",
						"/",
						new { controller = "index", action = "index" }
				);

				router.MapRoute
				(
						"mvc",
						"{controller}/{action?}",
						new { action = "index" }
				);
			});
		}

	}
}