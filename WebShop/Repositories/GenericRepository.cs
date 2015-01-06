using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;

namespace WebShop.Repositories
{
    /// <summary>
    /// Summary description for GenericRepository
    /// </summary>
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected DbContext Context;

        public GenericRepository(DbContext context)
        {
            Context = context;
        }
 
        public ICollection<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
 
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
 
        public TEntity Get(Object id)
        {
            return Context.Set<TEntity>().Find(id);
        }
 
        public async Task<TEntity> GetAsync(Object id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
 
        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().SingleOrDefault(match);
        }
 
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(match);
        }
 
        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().AsExpandable().Where(match).ToList();
        }
 
        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Context.Set<TEntity>().Where(match).ToListAsync();
        }
 
        public TEntity Add(TEntity t)
        {
            Context.Set<TEntity>().Add(t);
            Context.SaveChanges();
            return t;
        }
 
        public async Task<TEntity> AddAsync(TEntity t)
        {
            Context.Set<TEntity>().Add(t);
            await Context.SaveChangesAsync();
            return t;
        }
 
        public TEntity Update(TEntity updated,int key)
        {
            if (updated == null)
                return null;
 
            TEntity existing = Context.Set<TEntity>().Find(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }
            return existing;
        }
 
        public async Task<TEntity> UpdateAsync(TEntity updated, int key)
        {
            if (updated == null)
                return null;
            var existing = await Context.Set<TEntity>().FindAsync(key);
            if (existing == null) 
                return null;
            Context.Entry(existing).CurrentValues.SetValues(updated);
            await Context.SaveChangesAsync();
            return existing;
        }


        public async Task<TEntity> UpdateAsync(TEntity existing, TEntity updated)
        {
            if (updated == null || existing == null)
                return null;
            Context.Entry(existing).CurrentValues.SetValues(updated);
            await Context.SaveChangesAsync();
            return existing;
        }
 
        public void Delete(TEntity t)
        {
            Context.Set<TEntity>().Remove(t);
            Context.SaveChanges();
        }
 
        public async Task<int> DeleteAsync(TEntity t)
        {
            Context.Set<TEntity>().Remove(t);
            return await Context.SaveChangesAsync();
        }
 
        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }
 
        public async Task<int> CountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }
   
    }
}