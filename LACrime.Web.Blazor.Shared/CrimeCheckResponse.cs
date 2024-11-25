using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared {
    public class CrimeCheckResponse {
        public CrimeDto? CrimeDto { get; set; }
        public String? Message { get; set; }
        /// <summary>
        /// 1 - Crime found but description is different
        /// 2 - Crime found
        /// 3 - Crime not found and description is empty
        /// 4 - Crime will be created
        /// </summary>
        public CrimeCheckStatus Status { get; set; }
    }

    public enum CrimeCheckStatus {
        CrimeFoundDescDifferent = 1,
        CrimeFound = 2,
        CrimeNotFoundDescEmpty = 3,
        CrimeWillBeCreated = 4
    }
}
