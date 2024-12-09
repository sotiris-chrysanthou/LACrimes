using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared.VictimDtos {
    public class VictimDto {
        public int Age { get; set; }
        public string? Sex { get; set; }
        public string? Descent { get; set; }

        public VictimDto() { }

        public VictimDto(Victim victim) {
            Age = victim.Age;
            Sex = victim.Sex;
            Descent = victim.Descent;
        }
    }
}
