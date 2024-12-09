using LACrimes.EF.Repository;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Server.Authentication {
    public class UserAccountService {
        // Properties
        private List<Account>? _accounts = new List<Account>();

        // Constructors
        public UserAccountService() {
            SetAccounts();
        }

        private void SetAccounts() {
            Uri baseUrl = new Uri("https://localhost:5000");
            HttpClient httpClient = new HttpClient() {
                BaseAddress = baseUrl
            };
            AccountRepo accountRepo = new AccountRepo();
            var accounts = accountRepo.GetAll().Result;
            _accounts = accounts.ToList();
            
        }

        public Account? GetUserAccountByUsername(string username) {
            return _accounts.FirstOrDefault(a => a.Username == username);
        }
    }
}
