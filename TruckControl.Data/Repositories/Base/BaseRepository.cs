using System;
using System.Threading.Tasks;
using TruckControl.Model.EFModel;

namespace TruckControl.Data.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext AppDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            AppDbContext.Set<TEntity>().Add(entity);

            try
            {
                return await AppDbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int> UpdateAsync(TEntity entity)
        {

            try
            {
                AppDbContext.Set<TEntity>().Update(entity);
                return await AppDbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RemoveAsync(TEntity entity)
        {
            try
            {
                AppDbContext.Set<TEntity>().Remove(entity);
                return await AppDbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            AppDbContext.Dispose();
        }
    }
}
