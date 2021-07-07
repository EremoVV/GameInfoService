using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Contexts;
using Microsoft.AspNetCore.Authorization;
using GameInfoService.Catalog.Models.DTOs;
using GameInfoService.Catalog.Models.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameInfoService.Catalog.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CatalogController : ControllerBase
    {
        private GameInfoContext catalog;
        public CatalogController(GameInfoContext catalog)
        {
            this.catalog = catalog;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<string>> Index()
        {
            return Ok(new[]
            {
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9},
                new GameShortInfoDto{Name = "Death Stranding", Picture = "1.png", Rating = 7.5},
                new GameShortInfoDto{Name = "Red Dead Redemption 2", Picture = "2.png", Rating = 9}
            }.ToList());
        }

        [HttpGet]
        public ActionResult<string> Info()
        {
            return Ok("Catalog Info Action");
        }

        [HttpPost]
        public ActionResult<string> AddInfo(GameFullInfoDto gameFullInfo)
        {
            GameFullInfoDto gameInfoDTO = new GameFullInfoDto
            {
                Description = "desc",
                Name = "name",
                Picture = "some",
                Rating = 4,
                ReleaseDate = DateTime.Today
            };

            GameInfoEntity ent = new GameInfoEntity();

            gameInfoDTO.Adapt(ent);

            if (TryValidateModel(gameFullInfo))
            {

            }
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult<string> UpdateInfo()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public ActionResult<string> DeleteInfo()
        {
            throw new NotImplementedException();
        }
    }
}
