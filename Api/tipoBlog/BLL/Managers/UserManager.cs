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
    public class UserManager
    {
        IRepo<User> repo;
        IMapper mapper;
        public UserManager(IRepo<User> repo)
        {
            this.repo = repo;
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<User, UserDTO>()
                .ForMember(y => y.Publishers, y => y.Ignore())
                .ForMember(y => y.Subscribers, y => y.Ignore());

                x.CreateMap<Article, ArticleDTO>().ReverseMap();

                x.CreateMap<UserDTO, User>()
                .ForMember(y => y.Publishers, y => y.Ignore())
                .ForMember(y => y.Subscribers, y => y.Ignore());
            });
            mapper = new Mapper(configuration);
        }
        private Task<List<UserDTO>> ConvertFromSubAsync(List<Subscription> list, Func<Subscription, User> func)
        {
            Task<List<UserDTO>> task = new Task<List<UserDTO>>(() =>
            {
                if (list != null)
                {
                    var entityUsers = list.Select(func);
                    return new List<UserDTO>(mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entityUsers));
                }
                return null;
            });
            task.Start();
            return task;
        }
        private List<UserDTO> ConvertFromSub(List<Subscription> list, Func<Subscription, User> func)
        {
                if (list != null)
                {
                    var entityUsers = list.Select(func);
                    return new List<UserDTO>(mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entityUsers));
                }
                return null;
        }
        private Task<UserDTO> ConvertAsync(User entity)
        {
            Task<UserDTO> task = new Task<UserDTO>(() =>
            {
                UserDTO user = mapper.Map<User, UserDTO>(entity);
                user.Publishers =  ConvertFromSub(entity.Publishers, x => x.Publisher);
                user.Subscribers =  ConvertFromSub(entity.Subscribers, x => x.Subscriber);
                
                return user;
            });
            task.Start();
            return task;
        }

        private Task<User> ConvertAsync(UserDTO item)
        {
            Func<List<UserDTO>, List<Subscription>> func = (y) =>
            {
              if(y != null)
              {
                  var result = y.Select(x => new Subscription()
                  {
                      Publisher = mapper.Map<UserDTO, User>(x),
                      Subscriber = mapper.Map<UserDTO, User>(item),
                      PublisherId = x.UserId,
                      SubscriberId = item.UserId
                  });
                  return result.ToList();
              }
              return null;
            };
            Task <User> task = new Task<User>(() =>
            {
                User user = mapper.Map<UserDTO, User>(item);
                user.Publishers = func(item.Publishers);
                user.Subscribers = func(item.Subscribers);
                return user;
            });
            task.Start();
            return task;
        }
        public async Task<List<UserDTO>> GetSubs(int userId, int offset, int pageNumber)
        {
            var res = await (repo as UserRepo).GetSubs(userId, offset, pageNumber);
            var subs = await ConvertFromSubAsync(res, x => x.Subscriber);
            return subs;

        }
        public Task<List<UserDTO>> Search(string name)
        {
            return Task.Run(() =>
            {
                var res = repo.GetAll().Where(x => x.Name.ToLower().Contains(name.ToLower()));
                var users = mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(res);
                return users.ToList();
            });
        }
        public async Task<List<UserDTO>> GetPublishers(int userId, int offset, int pageNumber)
        {
            var res = await (repo as UserRepo).GetPublishers(userId, offset, pageNumber);
            var publishers = await ConvertFromSubAsync(res, x => x.Publisher);
            return publishers;
        }
       
        public Task<bool> ValidateLogin(string login)
        {
            Task<bool> task = new Task<bool>(() => !repo.GetAll().Any(x => x.Login == login));
            task.Start();
            return task;
        }

        public async Task<UserDTO> CreateAsync(UserDTO item)
        {
            User entity = await ConvertAsync(item);
            await repo.CreateAsync(entity);
            await repo.SaveAsync();
            return item;
        }
        public async Task<UserDTO> LoginAsync(string login, string passwordHash)
        {
            User user = repo.GetAll().FirstOrDefault(x => x.Login == login && x.PasswordHash == passwordHash);
            if (user is null)
                return null;
            UserDTO result = await ConvertAsync(user);
            return result;
        }
        public async Task<UserDTO> GetItemAsync(int id)
        {
            User entity = await repo.GetItemAsync(id);
            if (entity is null)
                return null;
            UserDTO user = await ConvertAsync(entity);
            return user;
        }


        public async Task<UserDTO> DeleteAsync(UserDTO user)
        {
            User entity = await repo.GetItemAsync(user.UserId);
            await repo.DeleteAsync(entity);
            await repo.SaveAsync();
           
            return user;
        }


        public async Task<UserDTO> UpdateAsync(UserDTO item)
        {
            User entity = await ConvertAsync(item);
            await repo.UpdateAsync(entity);
            await repo.SaveAsync();
            return item;
        }

    }
}
