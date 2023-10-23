using Data.Base;
using Data.DTO.Account;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SofttekWalletFront.Controllers
{
    [Authorize]
    /// <summary>
    /// Represents a controller for managing Accounts.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        /// <summary>
        /// Initializes a new instance of the AccountController class.
        /// </summary>
        /// <param name="httpAccount">IHttpAccountFactory</param>
        public AccountController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Displays the Account index view.
        /// </summary>
        /// <returns>The Account index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Deletes a Account with the specified code.
        /// </summary>
        /// <param name="id">An int.</param>
        /// <returns>Redirects to the Account index.</returns>
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            var response = await baseApi.DeleteToApi($"account/{id}", new { id = id }, token);

            var objectResponse = response as ObjectResult;

            return CreateResult(objectResponse);
        }

 

        /// <summary>
        /// Displays a partial view for adding or updating a Account.
        /// </summary>
        /// <param name="account">AccountDto</param>
        /// <returns>A partial view for adding or updating a Account.</returns>
        public IActionResult AccountAddPartial(AccountDto account)
        {


            var accountUpdate = new AccountViewModel()
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                UUID = account.UUID,
                Alias = account.Alias,
                Balance = account.Balance,
                CBU = account.CBU,
                ClientId = account.ClientId,
                Type = account.Type
            };

                return PartialView("~/Views/Account/Partial/AccountsAddPartial.cshtml", accountUpdate);
           
        }

        public IActionResult DepositPartial(AccountDto account)
        {


            var deposit = new AccountViewModel()
            {
                Type = account.Type,
                Balance = 0,
                Id = account.Id,
                ClientId= account.ClientId,
            };

            return PartialView("~/Views/Account/Partial/AccountDeposit.cshtml", deposit);

        }

        public async Task<IActionResult> AccountDeposit(AccountDto account)
        {


            account.Amount = account.Balance;
            var token = HttpContext.Session.GetString("Token");

            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PutToApi($"account/deposit/{account.Id}", account, token);
            var objectResponse = response as ObjectResult;

            return CreateResult(objectResponse);


        }

        public IActionResult ExtractPartial(AccountDto account)
        {


            var deposit = new AccountViewModel()
            {
                Type = account.Type,
                Balance = 0,
                Id = account.Id,
                ClientId = account.ClientId,
                
            };

            return PartialView("~/Views/Account/Partial/AccountExtract.cshtml", deposit);

        }

        public async Task<IActionResult> AccountExtract(AccountDto account)
        {

            account.Amount = account.Balance;

            var token = HttpContext.Session.GetString("Token");

            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PutToApi($"account/extract/{account.Id}", account, token);
            var objectResponse = response as ObjectResult;

            return CreateResult(objectResponse);


        }


        /// <summary>
        /// Creates a new Crypto account.
        /// </summary>
        /// <param name="Account">AccountCreateDto</param>
        /// <returns>Redirects to the Account index.</returns>
        public async Task<IActionResult> CreateAccountCrypto(AccountCreateDto account)
        {

            var token = HttpContext.Session.GetString("Token");

            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PostToApi("account/register", account, token);

            var objectResponse = response as ObjectResult;

            return CreateResult(objectResponse);

        }

        /// <summary>
        /// Creates a new Peso account.
        /// </summary>
        /// <param name="Account">AccountCreateDto</param>
        /// <returns>Redirects to the Account index.</returns>
        public async Task<IActionResult> CreateAccountPeso(AccountCreateDto account)
        {

            var token = HttpContext.Session.GetString("Token");

            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PostToApi("account/register", account, token);

            var objectResponse = response as ObjectResult;

            return CreateResult(objectResponse);

        }

        /// <summary>
        /// Creates a new Dollar account.
        /// </summary>
        /// <param name="Account">AccountCreateDto</param>
        /// <returns>Redirects to the Account index.</returns>
        public async Task<IActionResult> CreateAccountDollar(AccountCreateDto account)
        {

            var token = HttpContext.Session.GetString("Token");

            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PostToApi("account/register", account, token);

            var objectResponse = response as ObjectResult;

            return CreateResult(objectResponse);
        }



        public IActionResult CreateResult(ObjectResult objResponse)
        {
            
            if(objResponse.StatusCode == 200 || objResponse.StatusCode == 201)
            {
                ApiResponse jsonbject = JsonConvert.DeserializeObject<ApiResponse>(objResponse.Value.ToString());

                return Ok(jsonbject.Data);
            }

            var objResponseer = objResponse.Value.ToString();
            var jsonObject = JsonConvert.DeserializeObject<ApiErrorResponse>(objResponse.Value.ToString());

            return BadRequest(jsonObject.Errors[0].Error);


        }
    }
}
