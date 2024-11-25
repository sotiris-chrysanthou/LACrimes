using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class Victim {

        [Required] public Guid ID { get; set; }
        [Required] public int Age { get; set; }
        [StringLength(1)] public String? Sex { get; set; }
        [StringLength(1)] public String? Descent { get; set; }


        #region Relations
        public List<CrimeRecord> CrimeRecords { get; set; } = null!;
        #endregion

        #region Constructors
        public Victim() {
            
        }

        public Victim(int age, String sex, String descent) {
            ID = Guid.NewGuid();
            Age = age;
            Sex = sex;
            Descent = descent;
        }

        #endregion


    }
}
