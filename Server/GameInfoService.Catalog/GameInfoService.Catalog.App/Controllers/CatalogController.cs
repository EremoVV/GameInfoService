using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using GameInfoService.Catalog.Domain.Models.DTOs;
using GameInfoService.Catalog.Infrastructure.MappingInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameInfoService.Catalog.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Net.Http.Headers;

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
            if (string.IsNullOrEmpty(name)) return BadRequest("Name parameter is not defined");
            var gameInfo = _gameInfoService.GetGameInfoByName(name);
            if (gameInfo == null) return NotFound("No such game title");
            return Ok(_gameInfoMapper.MapToFullInfoDto(gameInfo));

        }

        [HttpPost]
        public ActionResult<string> AddGameInfo(GameInfoCreateDto gameInfo)
        {
            if (!TryValidateModel(gameInfo))
            {
                return BadRequest(ModelState);
            }

            _gameInfoService.AddGameInfo(_gameInfoMapper.MapToUdm(gameInfo));
            return Accepted($"{gameInfo.Name} added to the catalog");
        }

        [HttpPost]
        public ActionResult<string> UpdateGameInfo(GameInfoUpdateDto gameInfo)
        {
            try
            {
                _gameInfoService.UpdateGameInfo(_gameInfoMapper.MapToUdm(gameInfo));
                return Ok($"{gameInfo.Name} updated");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpDelete]
        public ActionResult<string> DeleteInfo(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest();
            var gameInfo = _gameInfoService.GetGameInfoByName(name);
            if (gameInfo == null) return NotFound("No such game title");
            try
            {
                _gameInfoService.RemoveGameInfo(name);
                return Ok($"Gameinfo {name} deleted");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<string>> Header()
        {
            var token2 = HttpContext.Request.Headers[HeaderNames.Authorization];
            return Ok(JsonSerializer.Serialize(HttpContext.Request.Headers));
        }
    }
}
