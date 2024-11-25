using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class Status {

        [Required] public Guid ID { get; set; }
        [Required] public String Code { get; set; } = null!;
        [Required] public String Desc { get; set; } = null!;


        #region Relations
        public List<CrimeRecord> CrimeRecords { get; set; } = null!;
        #endregion

        #region Constructors

        public Status() {
            
        }

        public Status(String code, String desc) {
            ID = Guid.NewGuid();
            Code = code;
            Desc = desc;
        }

        #endregion

    }
}
