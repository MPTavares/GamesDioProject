using MoviesDIOApi.Exceptions;
using MoviesDIOApi.Model;
using MoviesDIOApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository _repository;
        public GameService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<Game> AddGameAsync(GameModelInput gameInput)
        {
           
            Game game = null;
            game = await _repository.AddGameAsync(gameInput);
            return game;
        }

        public async Task<bool> DeleteGameByIdAsync(Guid id)
        {
            Game game = null;
            game = await _repository.GetGameByIdAsync(id);
            if (game != null)
            {
                await _repository.DeleteGameByIdAsync(id);
            } else
            {
                return false;
            }
            return true;
        }

        public async Task<List<Game>> GetAllAsync(Pagination pagination)
        {
            List<Game> gameList = new List<Game>();
            gameList = await _repository.GetAllAsync(pagination);

            return gameList;
        }

        public async Task<Game> GetGameByIdAsync(Guid id)
        {
            Game game = null;
            game = await _repository.GetGameByIdAsync(id);
            return game;
        }

        public async Task<Game> UpdateGameAsync(Guid id, GameModelInput gameInput)
        {
            Game game = null;
            game = await _repository.UpdateGameAsync(id, gameInput);
            return game;
        }

        public async Task<Game> UpdateValueGameAsync(Guid id, double price)
        {
            Game game = null;
            game = await _repository.UpdateValueGameAsync(id,price);
            return game;
        }
    }
}
