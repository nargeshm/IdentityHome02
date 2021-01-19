using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace session9_home.Models
{
    public class UserViewModel
    {
      
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required]
        [DataType(DataType.EmailAddress )]
        public string Email { get; set; }

    }
}
