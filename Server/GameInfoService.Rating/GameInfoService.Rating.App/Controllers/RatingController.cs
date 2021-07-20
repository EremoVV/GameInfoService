using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Rating.App.MappingInterfaces;
using GameInfoService.Rating.App.Middleware.ExceptionHandling.CustomExceptions;
using GameInfoService.Rating.Domain.Models.DTOs;
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
        public ActionResult<string> AddRating(GameInfoRatingCreateDto gameInfoRatingDto)
        {
            if (!TryValidateModel(gameInfoRatingDto)) return BadRequest(ModelState);
            _gameInfoRatingService.AddGameInfoRating(_gameInfoRatingMapper.MapToUdm(gameInfoRatingDto));
            return Ok("Added successfully");
        }
        [HttpPost]
        public ActionResult<string> UpdateRating(GameInfoRatingUpdateDto gameInfoRatingDto)
        {
            if (!TryValidateModel(gameInfoRatingDto)) return BadRequest(ModelState);
            try
            {
                _gameInfoRatingService.UpdateGameInfoRating(_gameInfoRatingMapper.MapToUdm(gameInfoRatingDto));
                return Ok("Updated successfully");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpDelete]
        public ActionResult<string> DeleteRating(int id)
        {
            try
            {
                _gameInfoRatingService.RemoveGameInfoRatingById(id);
                return Ok("Removed successfully");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
        [HttpGet]
        public ActionResult<string> GetAllRating()
        {
            return Ok(_gameInfoRatingService.GetAll());
        }

        [HttpGet]
        public ActionResult<string> Error()
        {
            _gameInfoRatingService.Error();
            return Ok("Error");
        }
    }
}
