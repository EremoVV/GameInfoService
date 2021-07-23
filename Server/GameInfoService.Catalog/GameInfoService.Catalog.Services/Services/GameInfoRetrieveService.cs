using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Domain.Models.Entities;
using GameInfoService.Catalog.Domain.Models.UDMs;
using GameInfoService.Catalog.Domain.RepositoryInterfaces;
using GameInfoService.Catalog.Infrastructure.MappingInterfaces;
using Mapster;


namespace GameInfoService.Catalog.Services.Services
{
    public class GameInfoRetrieveService : IGameInfoRetrieveService
    {
        private readonly IGameInfoRepository _gameInfoRepository;
        public GameInfoRetrieveService(IGameInfoRepository repository)
        {
            _gameInfoRepository = repository;
        }

        public async Task<IEnumerable<GameInfoUdm>> GetAllGameInfos()
        {
            return (await _gameInfoRepository
                .GetAllGameInfos())
                .Select(gameInfo => gameInfo.Adapt(new GameInfoUdm()))
                .ToList();
        }

        public async Task<GameInfoUdm> GetGameInfoByName(string name)
        {
            return (await _gameInfoRepository.GetAllGameInfos())
                    .FirstOrDefault(x => x.Name.Equals(name.Normalize()))
                    .Adapt(new GameInfoUdm());
            }

        public async Task<GameInfoUdm> GetGameInfo(int id)
        {
            return (await _gameInfoRepository.GetGameInfoById(id)).Adapt(new GameInfoUdm());
        }

        public async Task AddGameInfo(GameInfoUdm gameInfo)
        {
            await _gameInfoRepository
                .AddGameInfo(gameInfo.Adapt<GameInfoEntity>());
        }

        public async Task RemoveGameInfo(string name)
        {
            await _gameInfoRepository
                .RemoveGameInfo(name);
        }

        public async Task UpdateGameInfo(GameInfoUdm gameInfo)
        {
            await _gameInfoRepository.UpdateGameInfo(gameInfo.Adapt<GameInfoEntity>());
        }
    }
}
