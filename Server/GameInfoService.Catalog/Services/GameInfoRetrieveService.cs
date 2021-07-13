﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Models.DTOs;
using GameInfoService.Catalog.Models.Entities;
using GameInfoService.Catalog.Models.UDMs;
using GameInfoService.Catalog.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GameInfoService.Catalog.Services
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
            var gameFullInfoDtos = new List<GameInfoUdm>();
            foreach (var gameInfo in _gameInfoRepository.GetAllGameInfos())
            {
                gameFullInfoDtos.Add(gameInfo.Adapt(new GameInfoUdm()));
            }
            return gameFullInfoDtos;
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