using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class Subscription
    {
        
        public int SubscriberId { get; set; }
        public User Subscriber { get; set; }

       
        public int PublisherId { get; set; }
        public User Publisher { get; set; }
    }
}
