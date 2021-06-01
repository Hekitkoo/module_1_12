using System.Collections.Generic;
using ORM.Core.Models;

namespace ORM.Core.Interfaces
{
    public interface IRepository<TEntity, TId>
        where TEntity : Base
    {
        public void Create(TEntity entity);

        public TEntity GetById(TId entityId);
        public IEnumerable<TEntity> GetAll();
        public void Update(TEntity entity);
        public void Delete(TId entityId);
    }
}