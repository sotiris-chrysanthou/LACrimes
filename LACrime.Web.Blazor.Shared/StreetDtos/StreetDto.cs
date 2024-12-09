using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared.StreetDtos {
    public class StreetDto {
        public string Name { get; set; } = null!;

        public StreetDto() { }

        public StreetDto(Street street) {
            Name = street.Name;
        }
    }
}
