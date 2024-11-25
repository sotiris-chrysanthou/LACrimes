using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared {
    public class CrimeDto {
        public int Code { get; set; }
        public string Desc { get; set; } = null!;
        public CrimeDto() {

        }
        public CrimeDto(Crime crime) {
            Code = crime.Code;
            Desc = crime.Desc;
        }
    }
}
