using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Contexts;
using Microsoft.AspNetCore.Authorization;
using GameInfoService.Catalog.Models.DTOs;
using GameInfoService.Catalog.Models.Entities;
using GameInfoService.Catalog.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameInfoService.Catalog.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CatalogController : ControllerBase
    {
        private readonly IGameInfoRetrieveService _gameInfoService;
        public CatalogController(IGameInfoRetrieveService gameInfoService)
        {
            _gameInfoService = gameInfoService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<GameFullInfoDto>> Index()
        {
            return Ok(_gameInfoService.GetAllGameInfos());
        }

        [HttpGet]
        [Authorize]
        public ActionResult<GameFullInfoDto> GetGameInfoByName(string name)
        {
            return Ok(_gameInfoService.GetGameInfoByName(name));
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
