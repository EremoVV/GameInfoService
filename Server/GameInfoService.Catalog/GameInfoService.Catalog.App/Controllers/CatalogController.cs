using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyNetQ;
using GameInfoService.Catalog.Domain.Models.DTOs;
using GameInfoService.Catalog.Infrastructure.Config;
using GameInfoService.Catalog.Infrastructure.MappingInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameInfoService.Catalog.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GameInfoService.Catalog.App.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CatalogController : ControllerBase
    {
        private readonly IGameInfoRetrieveService _gameInfoService;
        private readonly IGameInfoMapper _gameInfoMapper;

        private readonly IOptions<RabbitMqConfig> _rabbitConfig;

        public CatalogController(IGameInfoRetrieveService gameInfoService, IGameInfoMapper gameInfoMapper, IOptions<RabbitMqConfig> rabbitConfig)
        {
            _gameInfoService = gameInfoService;
            _gameInfoMapper = gameInfoMapper;
            _rabbitConfig = rabbitConfig;

            Console.WriteLine("Constructed!");

            var brokerConnectionString = _rabbitConfig.Value.BrokerConnectionString;
            if (string.IsNullOrEmpty(brokerConnectionString)) brokerConnectionString = "host=localhost";
            var bus = RabbitHutch.CreateBus(brokerConnectionString);
            bus.PubSub.SubscribeAsync<RatingUpdateDto>("CatalogSub", UpdateRating);
            Console.WriteLine("Configured!");
            }

        private async Task UpdateRating(RatingUpdateDto ratingUpdate)
        {
            var updatedGameInfo = await _gameInfoService.GetGameInfo(ratingUpdate.GameId);
            updatedGameInfo.Rating = ratingUpdate.GameRating;
            await _gameInfoService.UpdateGameInfo(updatedGameInfo);
            Console.WriteLine($"Updated {ratingUpdate.GameId} with value: {ratingUpdate.GameRating}!");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GameFullInfoDto>>> Index()
        {
            return Ok(await _gameInfoService.GetAllGameInfos());
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GameFullInfoDto>> GetGameInfoByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Name parameter is not defined");
            var gameInfo = await _gameInfoService.GetGameInfoByName(name);
            if (gameInfo == null) return NotFound("No such game title");
            return Ok(_gameInfoMapper.MapToFullInfoDto(gameInfo));

        }

        [HttpPost]
        public async Task<ActionResult<string>> AddGameInfo(GameInfoCreateDto gameInfo)
        {
            if (!TryValidateModel(gameInfo))
            {
                return BadRequest(ModelState);
            }

            await _gameInfoService.AddGameInfo(_gameInfoMapper.MapToUdm(gameInfo));
            return Accepted($"{gameInfo.Name} added to the catalog");
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateGameInfo(GameInfoUpdateDto gameInfo)
        {
            try
            {
                await _gameInfoService.UpdateGameInfo(_gameInfoMapper.MapToUdm(gameInfo));
                return Ok($"{gameInfo.Name} updated");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteInfo(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest();
            var gameInfo = _gameInfoService.GetGameInfoByName(name);
            if (gameInfo == null) return NotFound("No such game title");
            try
            {
                await _gameInfoService.RemoveGameInfo(name);
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

        [HttpGet]
        public ActionResult<string> GetRabbitConfig()
        {
            return _rabbitConfig.Value.BrokerConnectionString;
        }
    }
}
