using MoviesDIOApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllAsync(Pagination pagination);
        Task<Game> GetGameByIdAsync(Guid id);
        Task<bool> DeleteGameByIdAsync(Guid id);
        Task<Game> AddGameAsync(GameModelInput game);
        Task<Game> UpdateGameAsync(Guid id, GameModelInput game);
        Task<Game> UpdateValueGameAsync(Guid id, double price);
    }
}
