using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class Premis {

        [Required] public Guid ID { get; set; }
        [Required] public int Code { get; set; }
        [Required] public string Desc { get; set; } = null!;


        #region Relations
        public List<CrimeRecord> CrimeRecords { get; set; } = null!;
        #endregion

        #region Constructors

        public Premis() {
            
        }

        public Premis(int code, string desc) {
            ID = Guid.NewGuid();
            Code = code;
            Desc = desc;
        }

        #endregion


    }
}
