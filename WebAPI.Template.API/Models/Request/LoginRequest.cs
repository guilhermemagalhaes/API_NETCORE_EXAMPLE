using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Template.API.Models.Request
{
    public class LoginRequest
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
