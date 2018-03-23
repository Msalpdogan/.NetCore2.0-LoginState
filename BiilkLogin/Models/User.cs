using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiilkLogin.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Passwordd { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public int Addresskey { get; set; }
    }
}
