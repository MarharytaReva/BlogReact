using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class ArticleRepo : RepoBase<Article>
    {
        public ArticleRepo(DbContext context) : base(context)
        {

        }
        public override async Task<Article> GetItemAsync(int id, int composeId = 0)
        {
            return await table.AsNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.ArticleId == id);
        }
        public async Task<List<Article>> GetUserArticles(int userId, int offset, int pageNumber)
        {
            return await (context as BlogContext).Articles.AsNoTracking().Include(x => x.User)
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.ArticleId)
                    .Skip((pageNumber - 1) * offset)
                    .Take(offset)
                    .ToListAsync();
        }
        public async Task<List<Article>> GetNews(int userId, int offset, int pageNumber)
        {
            if(userId == 0)
            {
                return await (context as BlogContext).Articles.AsNoTracking().Include(x => x.User)
                    .OrderByDescending(x => x.ArticleId)
                    .Skip((pageNumber - 1) * offset)
                    .Take(offset)
                    .ToListAsync();

            }
            else
            {
                var res = await (context as BlogContext).Articles.AsNoTracking().Include(y => y.User).Where(y =>
                ((context as BlogContext).Users
               .Include(x => x.Publishers)
               .FirstOrDefault(x => x.UserId == userId)
               .Publishers
               .Select(x => x.PublisherId).Contains(y.UserId))
                )
               .OrderByDescending(y => y.ArticleId)
               .Skip((pageNumber - 1) * offset)
               .Take(offset)
               .ToListAsync();
                return res;
            }
        }
    }
}
