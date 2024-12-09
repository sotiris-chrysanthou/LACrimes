using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared.SubAreaDtos;

namespace LACrimes.Web.Blazor.Shared.AreaDtos {
    public class AreaDto {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public IList<SubAreaDto> SubAreas { get; set; } = new List<SubAreaDto>();

        public AreaDto() {
            
        }

        public AreaDto(Area area) {
            Code = area.Code;
            Name = area.Name;
        }
    }
}
