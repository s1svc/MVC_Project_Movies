using CORE.Domain;
using Microsoft.EntityFrameworkCore;

namespace CORE.Services
{
    public abstract class DbService<TEntity> : Service, IDisposable where TEntity : Record, new()
    {
        private readonly DbContext _db;

        protected DbService(DbContext db)
        {
            _db = db;
        }
        protected virtual IQueryable<TEntity> DbQuery()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

        protected TEntity DbSingle(int id)
        {
            return DbQuery().AsTracking().SingleOrDefault(x => x.Id == id);
        }

        protected TEntity DbSingle(Func<TEntity, bool> predicate)
        {
            return DbQuery().AsTracking().SingleOrDefault(predicate);
        }

        protected virtual int DbSave() => _db.SaveChanges();

        protected void DbAdd(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Add(entity);
            if (save)
            {
                DbSave();
            }   
        }

        protected void DbUpdate(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save)
            {
                DbSave();
            }
        }

        protected void DbRemove(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Remove(entity);
            if (save)
            {
                DbSave();
            }
        }

        protected void DbRemove<TNavigation>(List<TNavigation> navigationEntities) where TNavigation : Record
        {
            _db.Set<TNavigation>().RemoveRange(navigationEntities);
        }

        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
