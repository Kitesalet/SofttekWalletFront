using Data.Base;
using Data.DTO;
using Data.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace IntegradorSofttekImanolFront.Controllers
{
    /// <summary>
    /// Represents a controller for managing user login and authentication.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        /// <summary>
        /// Initializes a new instance of the LoginController class.
        /// </summary>
        /// <param name="httpClient">IHTTpClientFactory</param>
        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Displays the login view.
        /// </summary>
        /// <returns>The login view.</returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Signs the user out and redirects to the login page.
        /// </summary>
        /// <returns>Redirects to the login page.</returns>
        public async Task<IActionResult> CloseSession()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// User login by POSTing loginDto to an API.
        /// </summary>
        /// <param name="login">LoginDto</param>
        /// <returns>
        /// If successful, redirects to the home page with a user token.
        /// |
        /// redirects to the login page with an error message.
        /// </returns>
        public async Task<IActionResult> Ingresar(LoginDto login)
        {
            var baseApi = new BaseApi(_httpClient);

            var token = await baseApi.PostToApi("login", login);

            var loginResult = token as OkObjectResult;

            if (loginResult == null)
            {
                ViewData["ErrorLogin"] = "Your credentials are incorrect";
                return RedirectToAction("Login");
            }

            var resultObject = JsonConvert.DeserializeObject<Login>(loginResult.Value.ToString());

            ViewData["Name"] = resultObject.Name;

            var handler = new JwtSecurityTokenHandler();
            var userToken = handler.ReadJwtToken(resultObject.Token);

            var claims = new List<Claim>();

            foreach (var claim in userToken.Claims)
            {
                if (claim.Type == ClaimTypes.Role || claim.Type == ClaimTypes.Name)
                {
                    claims.Add(claim);
                }

            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Email);

            identity.AddClaims(claims);

            var claimPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddHours(1)
            }); ;

            var homeViewModel = new HomeViewModel();
            homeViewModel.Token = resultObject.Token;

            HttpContext.Session.SetString("Token", resultObject.Token);
            return View("~/Views/Home/Index.cshtml", resultObject);
        }
    }
}
