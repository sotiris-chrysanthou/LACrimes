using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest9ReportDto))]
    public class Quest9ReportDto {
        public int AgeGroupStart { get; set; }
        public String? AgeGroup { get; set; }
        public String? WeaponCode { get; set; }
        public String? WeaponDescription { get; set; }
        public int Count { get; set; }
    }
}
