using Microsoft.EntityFrameworkCore;
using APIAUTH.Data.Context;
using APIAUTH.Domain.Repository;
using System.Linq.Expressions;
using APIAUTH.Domain.Entities;

namespace APIAUTH.Data.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ActivesWContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ActivesWContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Save();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return  _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
            return entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
