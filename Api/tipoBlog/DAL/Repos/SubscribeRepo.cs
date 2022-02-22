using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class SubscribeRepo : RepoBase<Subscription>
    {
        public SubscribeRepo(DbContext context) : base(context)
        {

        }
        public override async Task<Subscription> GetItemAsync(int id, int composeId)
        {
            return await table.FirstOrDefaultAsync(x => x.SubscriberId == id && x.PublisherId == composeId);
        }
    }
}
