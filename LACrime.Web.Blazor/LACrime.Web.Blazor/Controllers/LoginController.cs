using LACrimes.Web.Blazor.Server.Authentication;
using LACrimes.Web.Blazor.Shared.UserSession;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LACrimes.Web.Blazor.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        // Properties
        private UserAccountService _userAccount;

        // Constructors
        public LoginController(UserAccountService userAccount) {
            _userAccount = userAccount;
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] LoginRequest loginRequest) {
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userAccount);
            var userSession = jwtAuthenticationManager.GenarateJwtToken(loginRequest.Username, loginRequest.Password);
            if(userSession is null) {
                return Unauthorized();
            } else {
                return userSession;
            }
        }
    }
}
