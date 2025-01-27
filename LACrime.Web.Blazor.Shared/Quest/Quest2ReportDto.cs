﻿using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest2ReportDto))]
    public class Quest2ReportDto {
        public DateTime ReportDate { get; set; }
        public int TotalReports { get; set; }
    }
}
