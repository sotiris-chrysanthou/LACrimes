using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    public class Quest3ReportDto {
        public string? AreaCode { get; set; } = null;
        public string? AreaName { get; set; } = null;
        public string? CrimeCode { get; set; } = null;
        public string? CrimeDescription { get; set; } = null;
        public int TotalReports { get; set; }
    }
}
