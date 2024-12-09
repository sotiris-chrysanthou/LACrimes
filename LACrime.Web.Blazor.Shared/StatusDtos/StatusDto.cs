using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared.StatusDtos {
    public class StatusDto {
        public string Code { get; set; } = null!;
        public string Desc { get; set; } = null!;

        public StatusDto() { }

        public StatusDto(Status status) {
            Code = status.Code;
            Desc = status.Desc;
        }
    }
}
