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
    public class GenreService : DbService<Genre>, IService<GenreRequest, GenreResponse>
    {
        public GenreService(DbContext db) : base(db)
        {
        }

        public IQueryable<GenreResponse> Query()
        {
            return DbQuery().Select(x => new GenreResponse
            {
                Id = x.Id,
                Name = x.Name,
                MovieNamesR = string.Join(", ", x.MovieGenres.Select(y => y.Movie.Name))
            });
        }

        public CommandResponse Add(GenreRequest request)
        {
            if (DbQuery().Any(genreEnt => genreEnt.Name.ToLower()== request.Name.Trim().ToLower()))
                return Error($"\"{request.Name}\" genre exists!");
            var genreEnt = new Genre
            {
                Name = request.Name.Trim(),
            };
            DbAdd(genreEnt);
            return Success(genreEnt.Id,"Genre "+genreEnt.Name+" added.");
        }

        public GenreRequest Edit(int id)
        {
            var genreEnt = DbSingle(id);
            if (genreEnt == null)
                return null;
            
            return new GenreRequest
            {
                Id = genreEnt.Id,
                Name = genreEnt.Name
            }; 
        }

        public CommandResponse Remove(int id)
        {
            var genreEnt = DbQuery().Include(x => x.MovieGenres).AsTracking().SingleOrDefault(x => x.Id == id);
            if (genreEnt == null)
                return Error("Genre not found.");
            DbRemove(genreEnt.MovieGenres);
            DbRemove(genreEnt);
            return Success(genreEnt.Id, "Genre " + genreEnt.Name + " removed.");
        }

        public CommandResponse Update(GenreRequest request)
        {
            if (DbQuery().Any(genreEnt => genreEnt.Name.ToLower() == request.Name.Trim().ToLower() && genreEnt.Id != request.Id))
                return Error($"\"{request.Name}\" genre exists!");
            var genreEnt = DbSingle(request.Id);
            if (genreEnt == null)
                return Error("Genre not found.");
            genreEnt.Name = request.Name.Trim();
            DbUpdate(genreEnt);
            return Success(genreEnt.Id, "Genre " + genreEnt.Name + " updated.");
        }
    }
}
