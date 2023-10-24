using Data.Base;
using Data.DTO;
using Data.DTO.Client;
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
			/// Displays a partial view for adding or updating a client.
			/// </summary>
			/// <param name="client">ClientDto</param>
			/// <returns>A partial view for adding or updating a client.</returns>
			public IActionResult Register(ClientDto client)
			{

					var model = new ClientViewModel()
					{

					};
					return PartialView("~/Views/Login/Partial/Register.cshtml", model);
				
			}

		/// <summary>
		/// Creates a new client with the specified details.
		/// </summary>
		/// <param name="client">ClientDto.</param>
		/// <returns>Redirects to the client index.</returns>
		public async Task<IActionResult> CreateClient(ClientDto client)
		{
            if(client.Name == null)
            {
                client.Name = "";
            }
            if (client.Password == null)
            {
                client.Password = "";
            }
            if(client.Email == null)
            {
                client.Email = "";
            }

			var token = HttpContext.Session.GetString("Token");
			var baseApi = new BaseApi(_httpClient);
			var response = await baseApi.PostToApi("client/register", client, token);
            var objResponse = response as ObjectResult;

			if (objResponse.StatusCode == 200 || objResponse.StatusCode == 201)
			{
				ApiResponse jsonbject = JsonConvert.DeserializeObject<ApiResponse>(objResponse.Value.ToString());

				return Ok(jsonbject.Data);
			}

			var objResponseer = objResponse.Value.ToString();
			var jsonObject = JsonConvert.DeserializeObject<ApiErrorResponse>(objResponse.Value.ToString());

			return BadRequest(jsonObject.Errors[0].Error);

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

            var handler = new JwtSecurityTokenHandler();
            var userToken = handler.ReadJwtToken(resultObject.Token);

            var claims = new List<Claim>();

            foreach (var claim in userToken.Claims)
            {
                
                    claims.Add(claim);
                

            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.NameIdentifier);

            identity.AddClaims(claims);

            var claimPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddHours(1)
            }); ;

            var id = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Actor);

            var homeViewModel = new HomeViewModel();
            homeViewModel.Token = resultObject.Token;
            homeViewModel.Actor = int.Parse(id.Value);
            ;

            HttpContext.Session.SetString("Token", resultObject.Token);
            HttpContext.Session.SetString("Client", id.Value.ToString());
            return View("~/Views/Home/Index.cshtml", homeViewModel);
        }
    }
}
