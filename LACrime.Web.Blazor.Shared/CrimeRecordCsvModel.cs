using CsvHelper.Configuration;

namespace LACrimes.Web.Blazor.Shared {
    // Class for CSV data model
    public class CrimeRecordCsvModel {
        public string DrNo { get; set; } = string.Empty;
        public string DateRptd { get; set; } = string.Empty;
        public string DateOcc { get; set; } = string.Empty;
        public string TimeOcc { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string AreaName { get; set; } = string.Empty;
        public string RptDistNo { get; set; } = string.Empty;
        public string CrmCd { get; set; } = string.Empty;
        public string CrmCdDesc { get; set; } = string.Empty;
        public string Mocodes { get; set; } = string.Empty;
        public string VictAge { get; set; } = string.Empty;
        public string VictSex { get; set; } = string.Empty;
        public string VictDescent { get; set; } = string.Empty;
        public string PremisCd { get; set; } = string.Empty;
        public string PremisDesc { get; set; } = string.Empty;
        public string WeaponUsedCd { get; set; } = string.Empty;
        public string WeaponDesc { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusDesc { get; set; } = string.Empty;
        public string CrmCd1 { get; set; } = string.Empty;
        public string CrmCd2 { get; set; } = string.Empty;
        public string CrmCd3 { get; set; } = string.Empty;
        public string CrmCd4 { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string CrossStreet { get; set; } = string.Empty;
        public string Lat { get; set; } = string.Empty;
        public string Lon { get; set; } = string.Empty;
    }

    public sealed class CrimeRecordCsvModelMap : ClassMap<CrimeRecordCsvModel> {
        public CrimeRecordCsvModelMap() {
            Map(m => m.DrNo).Name("DR_NO");
            Map(m => m.DateRptd).Name("Date Rptd");
            Map(m => m.DateOcc).Name("DATE OCC");
            Map(m => m.TimeOcc).Name("TIME OCC");
            Map(m => m.Area).Name("AREA");
            Map(m => m.AreaName).Name("AREA NAME");
            Map(m => m.RptDistNo).Name("Rpt Dist No");
            Map(m => m.CrmCd).Name("Crm Cd");
            Map(m => m.CrmCdDesc).Name("Crm Cd Desc");
            Map(m => m.Mocodes).Name("Mocodes");
            Map(m => m.VictAge).Name("Vict Age");
            Map(m => m.VictSex).Name("Vict Sex");
            Map(m => m.VictDescent).Name("Vict Descent");
            Map(m => m.PremisCd).Name("Premis Cd");
            Map(m => m.PremisDesc).Name("Premis Desc");
            Map(m => m.WeaponUsedCd).Name("Weapon Used Cd");
            Map(m => m.WeaponDesc).Name("Weapon Desc");
    
            Map(m => m.Status).Name("Status");
            Map(m => m.StatusDesc).Name("Status Desc");
            Map(m => m.CrmCd1).Name("Crm Cd 1");
            Map(m => m.CrmCd2).Name("Crm Cd 2");
            Map(m => m.CrmCd3).Name("Crm Cd 3");
            Map(m => m.CrmCd4).Name("Crm Cd 4");
            Map(m => m.Location).Name("LOCATION");
            Map(m => m.CrossStreet).Name("Cross Street");
            Map(m => m.Lat).Name("LAT");
            Map(m => m.Lon).Name("LON");
        }
    }
}