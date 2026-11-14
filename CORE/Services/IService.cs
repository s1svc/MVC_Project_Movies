using CORE.Domain;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public interface IService<TRequest, TResponse> where TRequest : Record, new() where TResponse : Record, new()
    {
        public IQueryable<TResponse> Query();

        public List<TResponse> List() => Query().ToList();

        public TResponse Single(int id) => Query().SingleOrDefault(x => x.Id == id);

        public TRequest Edit(int id);

        public CommandResponse Add(TRequest request);
        public CommandResponse Update(TRequest request);
        public CommandResponse Remove(int id);
    }
   
}
