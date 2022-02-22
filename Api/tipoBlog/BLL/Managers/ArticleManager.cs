using AutoMapper;
using BLL.DTO;
using DAL.Context;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class ArticleManager
    {
        IMapper mapper;
        IRepo<Article> repo;
        public ArticleManager(IRepo<Article> repo)
        {
            this.repo = repo;
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Article, ArticleDTO>().ReverseMap();
                x.CreateMap<User, UserDTO>()
                .ForMember(y => y.Articles, y => y.Ignore())
                .ForMember(y => y.Subscribers, y => y.Ignore())
                .ForMember(y => y.Publishers, y => y.Ignore()).ReverseMap();
            });
            mapper = new Mapper(configuration);
        }
        public async Task<List<ArticleDTO>> GetNews(int userId, int offset, int pageNumber)
        {
            var articles = await (repo as ArticleRepo).GetNews(userId, offset, pageNumber);
            return await Task.Run(() => mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(articles).ToList());
        }
        public async Task<List<ArticleDTO>> GetUserArticles(int userId, int offset, int pageNumber)
        {
            var articles = await (repo as ArticleRepo).GetUserArticles(userId, offset, pageNumber);
            return await Task.Run(() => mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(articles).ToList());
        }
        private Task<ArticleDTO> ConvertAsync(Article enity)
        {
            Task<ArticleDTO> task = new Task<ArticleDTO>(() =>
            {
                return mapper.Map<Article, ArticleDTO>(enity);
            });
            task.Start();
            return task;
        }
        private Task<Article> ConvertAsync(ArticleDTO item)
        {
            Task<Article> task = new Task<Article>(() =>
            {
                return mapper.Map<ArticleDTO, Article>(item);
            });
            task.Start();
            return task;
        }


        public async Task<ArticleDTO> CreateAsync(ArticleDTO item)
        {
            Article entity = await ConvertAsync(item);
            await repo.CreateAsync(entity);
            await repo.SaveAsync();
            return item;
        }

        public async Task<ArticleDTO> GetItemAsync(int id)
        {
            Article entity = await repo.GetItemAsync(id);
            if (entity is null)
                return null;
            ArticleDTO res = await ConvertAsync(entity);
            return res;
        }

        public async Task<ArticleDTO> DeleteAsync(ArticleDTO article)
        {
            Article entity = await repo.GetItemAsync(article.ArticleId);
            await repo.DeleteAsync(entity);
            await repo.SaveAsync();
            return article;
        }


        public async Task<ArticleDTO> UpdateAsync(ArticleDTO item)
        {
            Article entity = await ConvertAsync(item);
            await repo.UpdateAsync(entity);
            await repo.SaveAsync();
            return item;
        }

    }
}
