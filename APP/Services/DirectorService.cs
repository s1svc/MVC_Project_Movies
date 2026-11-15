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
    public class DirectorService : DbService<Director>, IService<DirectorRequest, DirectorResponse>
    {
        public DirectorService(DbContext db) : base(db)
        {
        }

        // Includes the director's movies (needed by Remove's "has movies" check)
        // and orders by last name, then first name.
        protected override IQueryable<Director> DbQuery()
        {
            return base.DbQuery()
                .Include(directorEnt => directorEnt.Movies)
                .OrderBy(directorEnt => directorEnt.LastName)
                .ThenBy(directorEnt => directorEnt.FirstName);
        }

        public IQueryable<DirectorResponse> Query()
        {
            return DbQuery().Select(directorEnt => new DirectorResponse

            {
                Id = directorEnt.Id,
                FirstName = directorEnt.FirstName,
                LastName = directorEnt.LastName,
                IsRetired = directorEnt.IsRetired,
                FullNameR = directorEnt.FirstName + " " + directorEnt.LastName,
                IsRetiredR = directorEnt.IsRetired ? "Retired" : "Not Retired",
                MovieNamesR = string.Join(", ", directorEnt.Movies.Select(x=> x.Name))
            });
        }

        public CommandResponse Add(DirectorRequest request)
        {
            if (DbQuery().Any(x => x.FirstName == request.FirstName.Trim() &&
                        x.LastName == request.LastName.Trim()))
                return Error("Director exists!");
            var directorEnt = new Director
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                IsRetired = request.IsRetired
            };
            DbAdd(directorEnt);
            return Success(directorEnt.Id, "Director " + directorEnt.FirstName + " " + directorEnt.LastName + " added");
        }

        public DirectorRequest Edit(int id)
        {
            var directoryEnt = DbSingle(id);
            if (directoryEnt == null)
                return null;
            return new DirectorRequest
            {
                Id = directoryEnt.Id,
                FirstName = directoryEnt.FirstName,
                LastName = directoryEnt.LastName,
                IsRetired = directoryEnt.IsRetired
            };
        }

        public CommandResponse Remove(int id)
        {
            var directorEnt = DbSingle(id);
            if (directorEnt is null)
                return Error("Director not found.");

            if (directorEnt.Movies.Any())
                return Error("Director has movies.");

            DbRemove(directorEnt);

            return Success(directorEnt.Id, "Director deleted.");
        }

        public CommandResponse Update(DirectorRequest request)
        {
            if(DbQuery().Any(x => x.Id != request.Id && 
             x.FirstName == request.FirstName.Trim() && 
             x.LastName == request.LastName.Trim()))
                return Error("Director exists with different Id.");

            var directorEnt = DbSingle(request.Id);
            if (directorEnt == null)
                return Error("Director not found.");

            directorEnt.FirstName = request.FirstName.Trim();
            directorEnt.LastName = request.LastName.Trim();
            directorEnt.IsRetired = request.IsRetired;
            DbUpdate(directorEnt); 

            return Success(request.Id, "Director updated.");
        }
    }
}
