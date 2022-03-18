using System;
using System.Threading.Tasks;

namespace TruckControl.Data.Repositories.Base
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> RemoveAsync(TEntity entity);
    }
}
