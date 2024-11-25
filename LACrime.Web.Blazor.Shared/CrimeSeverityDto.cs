using System.ComponentModel.DataAnnotations;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared {
    public class CrimeSeverityDto {
        [Required] public int Code { get; set; }
        [Required] public string Desc { get; set; } = null!;
        [Required] public int Severity { get; set; }


        #region Relations
        [Required] public Guid CrimeID { get; set; }
        #endregion

        #region Constructors
        public CrimeSeverityDto() {
            
        }

        public CrimeSeverityDto(CrimeSeverity crmS) {
            Code = crmS.Crime.Code;
            Desc = crmS.Crime.Desc;
            Severity = crmS.Severity;
        }

        #endregion
    }
}
