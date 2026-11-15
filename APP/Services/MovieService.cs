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

        
        protected override IQueryable<Movie> DbQuery()
        {
            return base.DbQuery()
                .Include(movieEnt => movieEnt.Director)
                .Include(movieEnt => movieEnt.MovieGenres)
                .ThenInclude(movieGenreEnt => movieGenreEnt.Genre)
                .OrderByDescending(movieEnt => movieEnt.ReleaseDate)
                .ThenBy(movieEnt => movieEnt.Name);
        }
        public IQueryable<MovieResponse> Query()
        {
            return DbQuery().Select(x => new MovieResponse
            {
                Id = x.Id,
                Name = x.Name, 
                ReleaseDate = x.ReleaseDate,
                TotalRevenue = x.TotalRevenue,
                DirectorId = x.DirectorId,
                ReleaseDateR = x.ReleaseDate.HasValue ? x.ReleaseDate.Value.ToString("dd.MM.yyyy") : string.Empty,
                TotalRevenueR = x.TotalRevenue.ToString("C2"),
                DirectorNameR = x.Director.FirstName + " " + x.Director.LastName,
                GenreNamesR = string.Join("<br>", x.MovieGenres.OrderBy(x => x.Genre.Name).Select(x => x.Genre.Name))
            });
        }
        public CommandResponse Add(MovieRequest request)
        {
            if (DbQuery().Any(x => x.Name == request.Name.Trim()))
                return Error("Movie exists.");
            var movieEnt = new Movie
            {
                Name = request.Name.Trim(),
                ReleaseDate = request.ReleaseDate,
                TotalRevenue = request.TotalRevenue ?? 0,
                DirectorId = request.DirectorId ?? 0,
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
            if (DbQuery().Any(x => x.Id != request.Id && x.Name == request.Name.Trim()))
                return Error("Movie exists with different ID.");
            var movieEnt = DbSingle(request.Id);
            if (movieEnt == null)
                return Error("movie not found.");

            // Delete the old genre rows first, then set the new values and save.
            if (request.GenreIds is not null)
                DbRemove(movieEnt.MovieGenres);

            movieEnt.Name = request.Name.Trim();
            movieEnt.ReleaseDate = request.ReleaseDate;
            if (request.TotalRevenue.HasValue)
                movieEnt.TotalRevenue = request.TotalRevenue.Value;
            if (request.DirectorId.HasValue)
                movieEnt.DirectorId = request.DirectorId.Value;
            if (request.GenreIds is not null)
                movieEnt.GenreIds = request.GenreIds;
            DbUpdate(movieEnt);

            return Success(movieEnt.Id, "Movie updated");
        }
    }
}
