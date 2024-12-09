using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.UserSession {
    public class UserSession {
        // Properties
        public String Username { get; set; } = String.Empty;
        public String Token { get; set; } = String.Empty;
        public String Role { get; set; } = String.Empty;
        public int ExpiresIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
