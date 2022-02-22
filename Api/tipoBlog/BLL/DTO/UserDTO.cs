using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BLL.DTO
{
    
    public class UserDTO
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Your name is required")]
        [MaxLength(55, ErrorMessage = "Name must be less then 55 symbols")]
        public string Name { get; set; }



        [MaxLength(55, ErrorMessage = "Name must be less then 55 symbols")]
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "The password is required")]
        public string PasswordHash { get; set; }
        public string AvaBase { get; set; }

        
        public List<ArticleDTO> Articles { get; set; }



        public List<UserDTO> Subscribers { get; set; }
        public List<UserDTO> Publishers { get; set; } // <--
    }
}
