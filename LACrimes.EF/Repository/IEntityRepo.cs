using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.EF.Repository {
    public interface IEntityRepo<TEntity> {
        Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>>? predicate = null, bool IncludeAll = false);
        Task<TEntity?> GetById(Guid id);
        Task Add(TEntity entity);
        Task Update(Guid id, TEntity entity);
        Task Delete(Guid id);
    }
}
