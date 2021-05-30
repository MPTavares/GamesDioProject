using Microsoft.Extensions.Configuration;
using MoviesDIOApi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Repository
{
    public class GameRepository : IRepository, IDisposable
    {
        private readonly SqlConnection sqlConnection;
        private string _sqlCommandDelete = "delete From Games where game_id=@gameId";
        private string _sqlCommandSelectOne = "select game_id,game_name,game_producer,game_price From Games where game_id=@gameId";
        private string _sqlCommandSelectAll = "select game_id,game_name,game_producer,game_price From Games order by game_name OFFSET @PageSize * (@PageNumber - 1) ROWS  FETCH NEXT @PageSize ROWS ONLY;";
        private string _sqlCommandInsert = "insert into Games (game_name,game_producer,game_price) OUTPUT INSERTED.game_id values(@game_name,@game_producer,@game_price)";
        private string _sqlCommandUpdate = "update Games set game_name=@game_name,game_producer=@game_producer,game_price=@game_price where game_id=@gameId";
        private string _sqlCommandUpdatePriceOnly = "update Games set game_price=@game_price where game_id=@gameId";

        public GameRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("GamesDB"));            
        }
        public async Task<Game> AddGameAsync(GameModelInput gameInput)
        {
            await sqlConnection.OpenAsync();
            Game game = null;

            SqlCommand SqlCommand = new SqlCommand(_sqlCommandInsert, sqlConnection);
            SqlCommand.Parameters.Add("@game_name", System.Data.SqlDbType.VarChar).Value = gameInput.Name;
            SqlCommand.Parameters.Add("@game_producer", System.Data.SqlDbType.VarChar).Value = gameInput.Producer;
            SqlCommand.Parameters.Add("@game_price", System.Data.SqlDbType.SmallMoney).Value = gameInput.Price;
            Guid id = (Guid) await SqlCommand.ExecuteScalarAsync();
            if (id != null)
            {
                game = new Game
                {
                    Id = id,
                    Name = gameInput.Name,
                    Producer = gameInput.Producer,
                    Price = gameInput.Price
                };
            }
            await sqlConnection?.CloseAsync();
            return game;
        }

        public async Task<bool> DeleteGameByIdAsync(Guid id)
        {
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(_sqlCommandDelete, sqlConnection);
            sqlCommand.Parameters.Add("@gameId", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            int rowAffeced = await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection?.CloseAsync();
            return rowAffeced > 0;
        }

        public  async Task<List<Game>> GetAllAsync(Pagination pagination)
        {
            await sqlConnection.OpenAsync();
            Game game = null;
            List<Game> gameList = new List<Game>();
            SqlCommand sqlCommand = new SqlCommand(_sqlCommandSelectAll, sqlConnection);
            sqlCommand.Parameters.Add("@PageSize", System.Data.SqlDbType.Int).Value = pagination.PageSize;
            sqlCommand.Parameters.Add("@PageNumber", System.Data.SqlDbType.Int).Value = pagination.ActualPage;
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["game_id"],
                    Name = (string)sqlDataReader["game_name"],
                    Producer = (string)sqlDataReader["game_producer"],
                    Price = double.Parse(sqlDataReader["game_price"].ToString())
                };
                gameList.Add(game);
            }
            sqlConnection?.CloseAsync();
            return gameList;
        }

        public async Task<Game> GetGameByIdAsync(Guid id)
        {
            await sqlConnection.OpenAsync();
            Game game = null;
            SqlCommand sqlCommand = new SqlCommand(_sqlCommandSelectOne, sqlConnection);
            sqlCommand.Parameters.Add("@gameId", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            if (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["game_id"],
                    Name = (string)sqlDataReader["game_name"],
                    Producer = (string)sqlDataReader["game_producer"],
                    Price = double.Parse(sqlDataReader["game_price"].ToString())
                };
            }
            sqlConnection?.CloseAsync();
            return game;
        }

        public async Task<Game> UpdateGameAsync(Guid id, GameModelInput gameUpate)
        {
            await sqlConnection.OpenAsync();
            Game game = null;
            SqlCommand sqlCommand = new SqlCommand(_sqlCommandUpdate, sqlConnection);
            sqlCommand.Parameters.Add("@gameId", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            sqlCommand.Parameters.Add("@game_name", System.Data.SqlDbType.VarChar).Value = gameUpate.Name;
            sqlCommand.Parameters.Add("@game_producer", System.Data.SqlDbType.VarChar).Value = gameUpate.Producer;
            sqlCommand.Parameters.Add("@game_price", System.Data.SqlDbType.Money).Value = gameUpate.Price;
            int rowAffeced = await sqlCommand.ExecuteNonQueryAsync();
            if (rowAffeced > 0)
            {
                game = new Game
                {
                    Id = id,
                    Name = gameUpate.Name,
                    Producer = gameUpate.Producer,
                    Price = gameUpate.Price
                };
            }
            await sqlConnection?.CloseAsync();
            return game;
        }

        public async Task<Game> UpdateValueGameAsync(Guid id, double price)
        {
            Game game = null;
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(_sqlCommandUpdatePriceOnly, sqlConnection);
            sqlCommand.Parameters.Add("@gameId", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            sqlCommand.Parameters.Add("@game_price", System.Data.SqlDbType.Money).Value = price;
            int rowAffeced = await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection?.CloseAsync();
            if (rowAffeced > 0)
            {
                game = await GetGameByIdAsync(id);                
            }       
            return game;
        }

        public void Dispose()
        {            
            sqlConnection?.DisposeAsync();
        }
    }
}
