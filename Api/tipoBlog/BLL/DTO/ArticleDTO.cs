using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ArticleDTO
    {
        public int ArticleId { get; set; }
        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The plot is required")]
        public string Plot { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string ImageBase { get; set; }
       
        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
