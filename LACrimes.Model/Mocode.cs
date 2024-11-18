using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class Mocode {

        [Required] public Guid ID { get; set; }
        [Required] public string Code { get; set; } = null!;
        [Required] public string Desc { get; set; } = null!;

        #region Constructors

        public Mocode() {
            
        }

        public Mocode(string code, string desc) {
            ID = Guid.NewGuid();
            Code = code;
            Desc = desc;
        }

        #endregion



    }
}
