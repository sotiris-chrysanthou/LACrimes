using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class CrimeSeverity {
        [Required] public Guid ID { get; set; }
        [Required] public int Severity { get; set; }


        #region Relations
        [Required] public Guid CrimeID { get; set; }
        [Required] public Crime Crime { get; set; } = null!;
        [Required] public Guid CrimeRecordID { get; set; }
        [Required] public CrimeRecord CrimeRecord { get; set; } = null!;
        #endregion

    }
}
