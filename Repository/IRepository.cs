using MoviesDIOApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Repository
{
    public interface IRepository
    {
        Task<List<Game>> GetAllAsync(Pagination pagination);
        Task<Game> GetGameByIdAsync(Guid id);
        Task<bool> DeleteGameByIdAsync(Guid id);
        Task<Game> AddGameAsync(GameModelInput gameInput);
        Task<Game> UpdateGameAsync(Guid id, GameModelInput gameUpate);
        Task<Game> UpdateValueGameAsync(Guid id, double price);
    }
}
