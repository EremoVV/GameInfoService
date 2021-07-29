using GameInfoService.Catalog.Domain.Models.UDMs.GameDevs;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Catalog.Domain.Models.UDMs
{
    public class GameInfoUpdateUdm
    {
        public string Name { get; set; }
        public GameDeveloperUdm Developer { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
