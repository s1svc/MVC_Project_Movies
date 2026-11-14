using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Services
{
    public class MovieService : DbService<Movie>, IService<MovieRequest, MovieResponse>
    {
        public MovieService(DbContext db) : base(db)
        {
        }
        public IQueryable<MovieResponse> Query()
        {
            return DbQuery().Select(x => new MovieResponse
            {
                Id = x.Id,
                Name = x.Name, 
                ReleaseDate = x.ReleaseDate ?? DateTime.MinValue,
                TotalRevenue = x.TotalRevenue,
                DirectorId = x.DirectorId,
                RelaseDateR = x.ReleaseDate.ToString(),
                DirectorR = x.Director.FirstName + " " + x.Director.LastName,
                GenreNamesR = string.Join("<br>", x.MovieGenres.OrderBy(x => x.Genre.Name).Select(x => x.Genre.Name))
            });
        }
        public CommandResponse Add(MovieRequest request)
        {
            if (DbQuery().Any(x => x.Name == request.Name.Trim()))
                return Error("Movie exists.");
            var movieEnt = new Movie
            {
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                TotalRevenue = request.TotalRevenue,
                DirectorId = request.DirectorId,
                GenreIds = request.GenreIds
            };
            DbAdd(movieEnt);
            return Success(movieEnt.Id, "Movie added.");
        }

        public MovieRequest Edit(int id)
        {
            var movieEnt = DbSingle(id);
            if (movieEnt == null)
                return null;
            return new MovieRequest
            {
                Id = movieEnt.Id,
                Name = movieEnt.Name,
                ReleaseDate = movieEnt.ReleaseDate,
                TotalRevenue = movieEnt.TotalRevenue,
                DirectorId = movieEnt.DirectorId,
                GenreIds = movieEnt.GenreIds
            };
        }

      

        public CommandResponse Remove(int id)
        {
            var movieEnt = DbSingle(id);
            if (movieEnt == null)
                return Error("Movie not found.");
            DbRemove(movieEnt.MovieGenres);
            DbRemove(movieEnt);

            return Success(movieEnt.Id, "Movie deleted.");
        }   

        public CommandResponse Update(MovieRequest request)
        {
            if (DbQuery().Any(x => x.Id != request.Id && x.Name == request.Name))
                return Error("Movie exists with different ID.");
            var movieEnt = DbSingle(request.Id);
            if (movieEnt == null)
                return Error("movie not found.");

            if (!string.IsNullOrWhiteSpace(request.Name))
                movieEnt.Name = request.Name.Trim();
            if (request.ReleaseDate.HasValue)
                movieEnt.ReleaseDate = request.ReleaseDate;
            if (request.TotalRevenue > 0)
                movieEnt.TotalRevenue = request.TotalRevenue;
            if (request.DirectorId > 0)
                movieEnt.DirectorId = request.DirectorId;
            if (request.GenreIds != null && request.GenreIds.Any())
                movieEnt.GenreIds = request.GenreIds;

            return Success(movieEnt.Id, "Movie updated");
        }
    }
}
