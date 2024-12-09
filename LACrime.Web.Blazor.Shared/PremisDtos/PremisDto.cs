using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared.PremisDtos {
    public class PremisDto {
        public int Code { get; set; }
        public string? Desc { get; set; }

        public PremisDto() { }

        public PremisDto(Premis premis) {
            Code = premis.Code;
            Desc = premis.Desc;
        }
    }
}
