﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Models.Entities
{
    public class GameInfoEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public double Rating { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
