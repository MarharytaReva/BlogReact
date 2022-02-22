using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class SubscriptionDTO
    {
        [Required]
        public int SubscriberId { get; set; }
        public UserDTO Subscriber { get; set; }

        [Required]
        public int PublisherId { get; set; }
        public UserDTO Publisher { get; set; }
    }
}
