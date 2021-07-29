using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Rating.App.MappingInterfaces;
using GameInfoService.Rating.App.Middleware.ExceptionHandling.CustomExceptions;
using GameInfoService.Rating.Domain.Models.DTOs;
using GameInfoService.Rating.Domain.Models.UDMs;
using GameInfoService.Rating.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameInfoService.Rating.App.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class RatingController : ControllerBase
    {
        private readonly IGameInfoRatingService _gameInfoRatingService;
        private readonly IGameInfoRatingMapper _gameInfoRatingMapper;

        public RatingController(IGameInfoRatingService gameInfoRatingService, IGameInfoRatingMapper gameInfoRatingMapper)
        {
            _gameInfoRatingService = gameInfoRatingService;
            _gameInfoRatingMapper = gameInfoRatingMapper;
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddRating(GameInfoRatingCreateDto gameInfoRatingDto)
        {
            if (!TryValidateModel(gameInfoRatingDto)) return BadRequest(ModelState);
            await _gameInfoRatingService.AddGameInfoRating(_gameInfoRatingMapper.MapToUdm(gameInfoRatingDto));
            return Ok("Added successfully");
        }
        [HttpPost]
        public async Task<ActionResult<string>> UpdateRating(GameInfoRatingUpdateDto gameInfoRatingDto)
        {
            if (!TryValidateModel(gameInfoRatingDto)) return BadRequest(ModelState);
            try
            {
                await _gameInfoRatingService.UpdateGameInfoRating(_gameInfoRatingMapper.MapToUdm(gameInfoRatingDto));
                return Ok("Updated successfully");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteRating(int id)
        {
            try
            {
                await _gameInfoRatingService.RemoveGameInfoRatingById(id);
                return Ok("Removed successfully");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet] 
        public ActionResult<IEnumerable<GameInfoRatingUdm>> GetAllRating()
        {
            return Ok(_gameInfoRatingMapper.MapToDto(_gameInfoRatingService.GetAll()));
        }

        [HttpGet]
        public  async Task<ActionResult<GameInfoRatingDto>> GetGameInfoRating(string userId, int gameId)
        {
            return _gameInfoRatingMapper.MapToDto(await _gameInfoRatingService.GetGameInfoRating(userId, gameId));
        }


        [HttpGet]
        public async Task<ActionResult<string>> Error()
        {
            await _gameInfoRatingService.Error();
            return Ok("Error");
        }

        [HttpPost]
        public async Task<ActionResult<string>> AppendRating(GameInfoRatingDto ratingDto)
        {
            try
            {
                if (!TryValidateModel(ratingDto)) return BadRequest(ModelState);
                await _gameInfoRatingService.AppendGameInfoRating(_gameInfoRatingMapper.MapToUdm(ratingDto));
                return Ok($"{ratingDto.UserId} Appended rating");
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message} + {e.StackTrace}");
                return StatusCode(500, $"Message: {e.Message}, trace: {e.StackTrace}");
            }
        }
    }
}
