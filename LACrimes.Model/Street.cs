using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class Street {

        [Required] public Guid ID { get; set; }
        [Required] public String Name { get; set; } = null!;


        #region Relations
        public List<CrimeRecord> CrimeRecordsStreet { get; set; } = null!;
        public List<CrimeRecord> CrimeRecordsCrossStreet { get; set; } = null!;
        #endregion


        #region Constructors

        public Street() {
            
        }

        public Street(String name) { 
            ID = Guid.NewGuid();
            Name = name;
        }

        #endregion

    }
}
