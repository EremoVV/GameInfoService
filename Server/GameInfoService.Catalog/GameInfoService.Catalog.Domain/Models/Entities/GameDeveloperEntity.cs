using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Domain.Models.Entities
{
    public class GameDeveloperEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PicturePath { get; set; }
        //public int NumberOfFollowers { get; set; }
    }
}
