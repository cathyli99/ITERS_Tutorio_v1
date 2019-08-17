using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITERS_Tutorio_v1.Models
{
    public class Signin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ReturnKey
    {
        public string UID { get; set; }
        public string AuthToken { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
    }
}
