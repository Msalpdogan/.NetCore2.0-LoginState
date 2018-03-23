using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiilkLogin.Models
{
    public class SessionLayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Passwordd { get; set; }
        public string Mail { get; set; }
        public int Addresskey { get; set; }
    }
}
