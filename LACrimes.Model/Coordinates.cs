using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class Coordinates {

        [Required] public Guid ID { get; set; }
        [Required] public double Lat { get; set; }
        [Required] public double Lon { get; set; }


        #region Relations
        public List<CrimeRecord> CrimeRecords { get; set; } = null!;
        #endregion

        #region Constructors

        public Coordinates() {
            
        }

        public Coordinates(double lat, double lon) {
            ID = Guid.NewGuid();
            Lat = lat;
            Lon = lon;
        }



        #endregion

    }
}
