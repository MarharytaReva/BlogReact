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
    public class SubscribeManager
    {
        IRepo<Subscription> repo;
        IMapper mapper;
        public SubscribeManager(IRepo<Subscription> repo)
        {
            this.repo = repo;
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Subscription, SubscriptionDTO>().ReverseMap();
            });
            mapper = new Mapper(configuration);
        }
        private Task<SubscriptionDTO> ConvertAsync(Subscription enity)
        {
            Task<SubscriptionDTO> task = new Task<SubscriptionDTO>(() =>
            {
                return mapper.Map<Subscription, SubscriptionDTO>(enity);
            });
            task.Start();
            return task;
        }
        private Task<Subscription> ConvertAsync(SubscriptionDTO item)
        {
            Task<Subscription> task = new Task<Subscription>(() =>
            {
                return mapper.Map<SubscriptionDTO, Subscription>(item);
            });
            task.Start();
            return task;
        }


        public async Task<SubscriptionDTO> CreateAsync(SubscriptionDTO item)
        {
            Subscription entity = await ConvertAsync(item);
            await repo.CreateAsync(entity);
            await repo.SaveAsync();
            return item;
        }

        public async Task<SubscriptionDTO> GetItemAsync(int subId, int publishrId)
        {
            Subscription entity = await repo.GetItemAsync(subId, publishrId);
            if (entity is null)
                return null;
            SubscriptionDTO sub = await ConvertAsync(entity);
            return sub;
        }

        public async Task<SubscriptionDTO> DeleteAsync(SubscriptionDTO subscription)
        {
            Subscription entity = await repo.GetItemAsync(subscription.SubscriberId, subscription.PublisherId);
            await repo.DeleteAsync(entity);
            await repo.SaveAsync();
            SubscriptionDTO item = await ConvertAsync(entity);
            return item;
        }


        public async Task<SubscriptionDTO> UpdateAsync(SubscriptionDTO item)
        {
            Subscription entity = await ConvertAsync(item);
            await repo.UpdateAsync(entity);
            await repo.SaveAsync();
            return item;
        }
    }
}
