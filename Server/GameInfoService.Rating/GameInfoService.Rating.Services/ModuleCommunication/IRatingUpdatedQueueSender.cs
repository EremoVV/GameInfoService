using GameInfoService.Rating.Domain.Models.DTOs;

namespace GameInfoService.Rating.Services.ModuleCommunication
{
    public interface IRatingUpdatedQueueSender
    {
        string SendUpdateRatingQueue(UpdateRatingDto updateRatingInfo);
    }
}
