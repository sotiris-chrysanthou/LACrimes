using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared.WeaponDtos {
    public class WeaponDto {
        public int Code { get; set; }
        public string Desc { get; set; } = null!;

        public WeaponDto() { }

        public WeaponDto(Weapon weapon) {
            Code = weapon.Code;
            Desc = weapon.Desc;
        }
    }
}
