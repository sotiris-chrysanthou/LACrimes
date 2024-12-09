using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using static LACrimes.Model.Enums.Enums;

namespace LACrimes.Web.Blazor.Shared {
    public class AccountDto {
        [Required] public Guid ID { get; set; }
        [Required] public String Username { get; set; } = String.Empty;
        [Required] public String Password { get; set; } = String.Empty;
        [Required] public UserType Role { get; set; }

        // Constructors
        public AccountDto() {

        }

        public AccountDto(Account account) {
            ID = account.ID;
            Username = account.Username;
            Password = account.Password;
            Role = account.Role;
        }
    }
}
