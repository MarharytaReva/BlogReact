using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class UserRepo : RepoBase<User>
    {
        public UserRepo(DbContext context) : base(context)
        {

        }
        public override async Task<User> GetItemAsync(int id, int composeId = 0)
        {
            return await table.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
        }
        public async Task<List<Subscription>> GetSubs(int userId, int offset, int pageNumber)
        {
            var user = await base.GetItemAsync(userId);
            if (user != null)
            {
                var collection = await context.Entry(user)
                    .Collection(p => p.Subscribers)
                    .Query()
                    .OrderBy(x => x.Subscriber.Name)
                    .Skip((pageNumber - 1) * offset)
                    .Take(offset)
                    .ToListAsync();
                collection.ForEach(x => context.Entry(x).Reference(y => y.Subscriber).Load());
                return collection;
            }
            return null;
        }
        public async Task<List<Subscription>> GetPublishers(int userId, int offset, int pageNumber)
        {
            var user = await base.GetItemAsync(userId);
            if (user != null)
            {
                var collection = await context.Entry(user)
                    .Collection(p => p.Publishers)
                    .Query()
                    .OrderBy(x => x.Publisher.Name)
                    .Skip((pageNumber - 1) * offset)
                    .Take(offset)
                    .ToListAsync();
                collection.ForEach(x => context.Entry(x).Reference(y => y.Publisher).Load());
                return collection;
            }
            return null;
        }
    }
}
