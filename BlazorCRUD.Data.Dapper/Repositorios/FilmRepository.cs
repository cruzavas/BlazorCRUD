using BlazorCRUD.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCRUD.Data.Dapper.Repositorios
{
	public class FilmRepository : IFilmRepository
	{
		private string _connectionString;
		public FilmRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		protected SqlConnection dbConnection()
		{
			return new SqlConnection(_connectionString);
		}
		public async Task<bool> DeleteFilm(int id)
		{
			var db = dbConnection();
			var sql = "DELETE FROM Films WHERE Id = @Id";

			var result = await db.ExecuteAsync(sql, new { Id = id });

			return result > 0;
		}

		public async Task<IEnumerable<Film>> GetAllFilms()
		{
			var db = dbConnection();

			var sql = "SELECT Id, Title, Director, ReleaseDate FROM [dbo].[Films]";

			return await db.QueryAsync<Film>(sql, new { });
		}

		public async Task<Film> GetFilmDetail(int id)
		{
			var db = dbConnection();
			var sql = "SELECT Id, Title, Director, ReleaseDate " +
				"FROM [dbo].[Films] WHERE Id = @Id";

			return await db.QueryFirstOrDefaultAsync<Film>(sql, new { Id = id });
		}

		public async Task<bool> InsertFilm(Film film)
		{
			var db = dbConnection();
			var sql = "INSERT INTO Films (Title, Director, ReleaseDate)" +
				"VALUES(@Title, @Director, @ReleaseDate)";

			var result = await db.ExecuteAsync(sql, new { film.Title, 
				film.Director, film.ReleaseDate });

			return result > 0;
		}

		public async Task<bool> UpdateFilm(Film film)
		{
			var db = dbConnection();
			var sql = "UPDATE Films SET Title = @Title, " +
				"Director = @Director, ReleaseDate = @ReleaseDate " +
				"WHERE Id = @Id";

			var result = await db.ExecuteAsync(sql, 
				new { film.Title, film.Director, film.ReleaseDate, film.Id });

			return result > 0;
		}
	}
}
