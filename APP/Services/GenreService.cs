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

        public CommandResponse Add(GenreRequest request)
        {
            throw new NotImplementedException();
        }

        public GenreResponse Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GenreResponse> Query()
        {
            return DbQuery().Select(x => new GenreResponse
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        public CommandResponse Remove(int id)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Update(GenreRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
