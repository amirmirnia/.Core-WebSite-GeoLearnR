using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class GenericRepository<TEntity> : IGeneric<TEntity> where TEntity : class
    {
        private ContextTMU _db;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(ContextTMU db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void DeleteWhitList(List<int> IdFile)
        {
            foreach (var item in IdFile)
            {

                _dbSet.Remove(GetById(item));
                _db.SaveChanges();
            }
        }

        public TEntity GetById(int Id)
        {
            return _dbSet.Find(Id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (where != null)
            {
                query = query.Where(where);
            }

            return query.ToList();
        }

        public TEntity GetEntity(Expression<Func<TEntity, bool>> where = null)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
