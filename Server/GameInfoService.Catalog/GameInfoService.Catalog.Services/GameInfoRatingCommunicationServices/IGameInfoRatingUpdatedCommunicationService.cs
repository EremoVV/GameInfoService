using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Services.GameInfoRatingCommunicationServices
{
    public interface IGameInfoRatingUpdatedCommunicationService
    {
        Task ConsumeUpdatedRating();
    }
}
