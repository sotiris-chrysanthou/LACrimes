using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LACrimes.Model.Enums.Enums;

namespace LACrimes.Model {
    public class Account {
        [Required] public Guid ID { get; set; }
        [Required] public String Username { get; set; } = String.Empty;
        [Required] public String Password { get; set; } = String.Empty;
        [Required] public UserType Role { get; set; }


        // Constructors
        public Account() {

        }

        public Account(Guid id, String username, String password, UserType role) {
            ID = id;
            Username = username;
            Password = password;
            Role = role;
        }
    }
}
