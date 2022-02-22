using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class RepoBase<T> : IRepo<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> table;
        public RepoBase(DbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public async Task CreateAsync(T item)
        {
            await table.AddAsync(item);
        }
        public Task DeleteAsync(T item)
        {
            return Task.Factory.StartNew(() =>
            {
                table.Remove(item);
            });
        }
        public IQueryable<T> GetAll()
        {
            return table;
        }
        public virtual async Task<T> GetItemAsync(int id, int composeId = 0)
        {
            return await table.FindAsync(id);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        public Task UpdateAsync(T item)
        {
            return Task.Factory.StartNew(() =>
            {
                table.Update(item);
            });
        }
    }
}
