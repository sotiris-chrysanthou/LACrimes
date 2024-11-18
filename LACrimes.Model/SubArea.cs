using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class SubArea {
        [Required] public Guid ID { get; set; }
        [Required] public int RpdDistNo { get; set; }

        #region Relations
        public Guid? AreaID { get; set; }
        public Area? Area { get; set; } = null;

        public List<CrimeRecord> CrimeRecords { get; set; } = null!;
        #endregion

        #region Constructors

        public SubArea() {
                
        }

        public SubArea(int rpdDistNo, Guid? areaID) {
            ID = Guid.NewGuid();
            RpdDistNo = rpdDistNo;
            AreaID = areaID;
        }

        #endregion
    }
}
