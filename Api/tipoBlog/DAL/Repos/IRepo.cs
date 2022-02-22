using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IRepo<T> where T : class
    {
        IQueryable<T> GetAll();
        
        Task DeleteAsync(T item);
       
        Task CreateAsync(T item);
       
        Task UpdateAsync(T item);
        
        Task SaveAsync();
       
        Task<T> GetItemAsync(int id, int composeId = 0);
    }
}
