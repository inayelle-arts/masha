﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace App.DAL.Entities
{
    public class ConstraintRecord
    {
        public int Id { get; set; }
        public Task Task { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}