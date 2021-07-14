using System.Collections.Generic;
using System.Linq;
using GameInfoService.Catalog.Domain.Models.Entities;
using GameInfoService.Catalog.Domain.Models.UDMs;
using GameInfoService.Catalog.Domain.RepositoryInterfaces;
using GameInfoService.Catalog.Infrastructure.MappingInterfaces;


namespace GameInfoService.Catalog.Services.Services
{
    public class GameInfoRetrieveService : IGameInfoRetrieveService
    {
        private readonly IGameInfoRepository _gameInfoRepository;
        public GameInfoRetrieveService(IGameInfoRepository repository)
        {
            _gameInfoRepository = repository;
        }

        public IEnumerable<GameInfoUdm> GetAllGameInfos()
        {
            return _gameInfoRepository.GetAllGameInfos().Select(gameInfo => gameInfo.Adapt(new GameInfoUdm())).ToList();
        }

        public GameInfoUdm GetGameInfoByName(string name)
        {
            return _gameInfoRepository.GetAllGameInfos()
                    .FirstOrDefault(x => x.Name.Normalize().Equals(name.Normalize())).Adapt(new GameInfoUdm());
            }

        public void AddGameInfo(GameInfoUdm gameInfo)
        {
            _gameInfoRepository.AddGameInfo(gameInfo.Adapt<GameInfoEntity>());
        }

        public void RemoveGameInfo(GameInfoUdm gameInfo)
        {
            _gameInfoRepository.RemoveGameInfo(gameInfo.Adapt<GameInfoEntity>());
        }
    }
}
