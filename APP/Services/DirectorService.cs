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

        public IQueryable<DirectorResponse> Query()
        {
            return DbQuery().Select(directorEnt => new DirectorResponse

            {
                Id = directorEnt.Id,
                FirstName = directorEnt.FirstName,
                LastName = directorEnt.LastName,
                isRetired = directorEnt.isRetired,
                FullNameR = directorEnt.FirstName + " " + directorEnt.LastName,
                isRetiredR = directorEnt.isRetired ? "Retired" : "Not Retired",
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
                FirstName = request.FirstName,
                LastName = request.LastName,
                isRetired = request.IsRetired
            };
            DbAdd(directorEnt);
            return Success(request.Id, "Director " + request.FirstName + " " + request.LastName + " added");
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
                IsRetired = directoryEnt.isRetired
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

            directorEnt.FirstName = request.FirstName;
            directorEnt.LastName = request.LastName;
            directorEnt.isRetired = request.IsRetired;
            DbUpdate(directorEnt); 

            return Success(request.Id, "Director updated.");
        }
    }
}
