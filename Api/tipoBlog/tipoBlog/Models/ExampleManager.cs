using DAL.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace tipoBlog.Models
{
    public static class ExampleManager
    {
       
        static void Serialize<T>(IList<T> list, string fileName)
        {
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(fileName, json);
        }
        private static IList<T> Deserialize<T>(string fileName)
        {
            List<T> list = new List<T>();
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                string json = File.ReadAllText(fileName);
                list = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return list;
        }
        public static void FillData(BlogContext context)
        {
            if (!context.Users.Any())
            {
                List<User> users = Deserialize<User>("users.json").ToList();
                foreach (var item in users)
                    item.PasswordHash = CryptoService.getHash("84l56SCp");
                List<Article> articles1 = Deserialize<Article>("articles.json").ToList();
                for(int i = 0; i < articles1.Count; i++)
                {
                    articles1[i].Plot = (articles1[i].Plot.Replace('\'', ' ').Replace('\"', ' '));
                    articles1[i].Title = (articles1[i].Title.Replace('\'', ' ').Replace('\"', ' '));
                }
               

               
                List<Article> articles2 = Deserialize<Article>("articles.json").ToList();
                for (int i = 0; i < articles2.Count; i++)
                {
                    articles2[i].Plot = (articles2[i].Plot.Replace('\'', ' ').Replace('\"', ' '));
                    articles2[i].Title = (articles2[i].Title.Replace('\'', ' ').Replace('\"', ' '));
                }

                User ya = users.FirstOrDefault(x => x.Login == "reva.marharyta@gmail.com");
                User user = users.FirstOrDefault(x => x.Login == "kiano@gamil.com");
                context.Users.AddRange(users);

                for(int i = 0; i < articles1.Count; i++)
                {
                    articles1[i].User = ya;
                    articles2[i].User = user;
                    context.Articles.Add(articles1[i]);
                    context.Articles.Add(articles2[i]);
                }
                List<Subscription> subscriptions1 = new List<Subscription>();
                List<Subscription> subscriptions2 = new List<Subscription>();
                List<User> subs1 = users.Where(x => x.Login != ya.Login).ToList();
                List<User> subs2 = users.Where(x => x.Login != user.Login).ToList();

                foreach(var sub in subs1)
                {
                    Subscription subscription = new Subscription()
                    {
                        Subscriber = sub,
                        Publisher = ya
                    };
                    subscriptions1.Add(subscription);
                }
                foreach (var sub in subs2)
                {
                    Subscription subscription = new Subscription()
                    {
                        Subscriber = sub,
                        Publisher = user
                    };
                    subscriptions2.Add(subscription);
                }
                context.Subscriptions.AddRange(subscriptions1);
                context.Subscriptions.AddRange(subscriptions2);
                context.SaveChanges();

            }
        }
    }
}
