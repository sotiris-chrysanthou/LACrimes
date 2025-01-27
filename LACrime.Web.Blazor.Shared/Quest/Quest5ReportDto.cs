﻿using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest5ReportDto))]
    public class Quest5ReportDto {
        public String? CrmCd { get; set; }
        public int TotalReports { get; set; }
    }
}
