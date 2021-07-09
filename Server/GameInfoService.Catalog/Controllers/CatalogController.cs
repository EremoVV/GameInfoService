using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Contexts;
using GameInfoService.Catalog.MappingInterfaces;
using Microsoft.AspNetCore.Authorization;
using GameInfoService.Catalog.Models.DTOs;
using GameInfoService.Catalog.Models.Entities;
using GameInfoService.Catalog.Models.UDMs;
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
        private readonly IGameInfoMapper _gameInfoMapper;
        public CatalogController(IGameInfoRetrieveService gameInfoService, IGameInfoMapper gameInfoMapper)
        {
            _gameInfoService = gameInfoService;
            _gameInfoMapper = gameInfoMapper;
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

        [HttpPost]
        public ActionResult<string> AddInfo(GameFullInfoDto gameInfo)
        {
            if (!TryValidateModel(gameInfo))
            {
                return BadRequest(ModelState);
            }
            else
            {
                _gameInfoService.AddGameInfo(_gameInfoMapper.MapToUdm(gameInfo));
                return Accepted();
            }
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
