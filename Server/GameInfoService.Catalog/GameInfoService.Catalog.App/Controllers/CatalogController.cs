using System;
using System.Collections.Generic;
using GameInfoService.Catalog.Domain.Models.DTOs;
using GameInfoService.Catalog.Infrastructure.MappingInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameInfoService.Catalog.Services.Services;

namespace GameInfoService.Catalog.App.Controllers
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
            if (!string.IsNullOrEmpty(name))
            {
                var gameInfo = _gameInfoService.GetGameInfoByName(name);
                if (gameInfo == null) return NotFound("No such game title");
                return Ok(_gameInfoMapper.MapToFullInfoDto(gameInfo));
            }

            return BadRequest("Name parameter is not defined");
        }

        [HttpPost]
        public ActionResult<string> AddInfo(GameFullInfoDto gameInfo)
        {
            if (!TryValidateModel(gameInfo))
            {
                return BadRequest(ModelState);
            }

            _gameInfoService.AddGameInfo(_gameInfoMapper.MapToUdm(gameInfo));
            return Accepted();
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
