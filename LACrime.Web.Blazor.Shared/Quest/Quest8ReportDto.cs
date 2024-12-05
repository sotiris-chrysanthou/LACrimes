using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest8ReportDto))]
    public class Quest8ReportDto {
        public String? Crime1Code { get; set; }
        public String? Crime1Description { get; set; }
        public String? Crime2Code { get; set; }
        public String? Crime2Description { get; set; }
        public int CrimeCount { get; set; }
    }
}
