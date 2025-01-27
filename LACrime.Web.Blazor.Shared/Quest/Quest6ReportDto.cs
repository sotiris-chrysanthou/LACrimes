﻿using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest6ReportDto))]
    public class Quest6ReportDto {
        public List<Quest6AreaDto> TopAreas { get; set; } = new();
        public List<Quest6RptDistNoDto> TopRptDistNos { get; set; } = new();
    }

    public class Quest6AreaDto {
        public String AreaName { get; set; } = string.Empty;
        public DateTime DateOccurred { get; set; }
        public int TotalCrimes { get; set; }
    }

    public class Quest6RptDistNoDto {
        public String RptDistNo { get; set; } = string.Empty;
        public DateTime DateOccurred { get; set; }
        public int TotalCrimes { get; set; }
    }

}
