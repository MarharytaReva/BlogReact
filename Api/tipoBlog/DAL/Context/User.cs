using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string AvaBase { get; set; }

        [InverseProperty("User")]
        public List<Article> Articles { get; set; }



        public List<Subscription> Subscribers { get; set; }
        public List<Subscription> Publishers { get; set; } // <--
    }
}
