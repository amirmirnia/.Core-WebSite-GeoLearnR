using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface IGeneric<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null);

        TEntity GetEntity(Expression<Func<TEntity, bool>> where = null);
        TEntity GetById(int Id);

        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);

        void  Update(TEntity entity);

        void Delete(int id);
        void Delete(TEntity entity);

        #region File
        void DeleteWhitList(List<int> IdFile);

        #endregion
    }
}
