using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.UserSession {
    public class LoginRequest {
        public String Username { get; set; } = String.Empty;
        public String Password { get; set; } = String.Empty;
    }
}
