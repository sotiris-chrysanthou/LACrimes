using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared.AreaDtos;

namespace LACrimes.Web.Blazor.Shared.SubAreaDtos {
    public class SubAreaDto {
        public string RpdDistNo { get; set; } = null!;
        public AreaDto Area { get; set; } = null!;

        public SubAreaDto() { }

        public SubAreaDto(SubArea subArea) {
            RpdDistNo = subArea.RpdDistNo;
            Area = new AreaDto(subArea.Area);
        }
    }
}
