using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using GameInfoService.Rating.Domain.Models.DTOs;

namespace GameInfoService.Rating.Services.Communication
{
    public class GameInfoCatalogCommunicationService
    {
        public async Task SendUpdateRatingMessage(UpdateRatingDto updateRatingDto)
        {
            using var bus = RabbitHutch.CreateBus("host=localhost");
            await bus.PubSub.PublishAsync(updateRatingDto);
        }
    }
}
