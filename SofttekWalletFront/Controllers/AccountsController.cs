using BilleteraVirtualSofttekBack.Models.DTOs.Account;
using Data.Base;
using Data.DTO.Account;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SofttekWalletFront.Controllers
{
    [Authorize]
    /// <summary>
    /// Represents a controller for managing Accounts.
    /// </summary>
    public class AccountsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        /// <summary>
        /// Initializes a new instance of the AccountController class.
        /// </summary>
        /// <param name="httpAccount">IHttpAccountFactory</param>
        public AccountsController(IHttpClientFactory httpClient)
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
        public IActionResult DeleteAccount(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            var usuarios = baseApi.DeleteToApi($"account/{id}", new { id = id }, token);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays a partial view for adding or updating a Account.
        /// </summary>
        /// <param name="Account">AccountDto</param>
        /// <returns>A partial view for adding or updating a Account.</returns>
        public IActionResult AccountsAddPartial(AccountDto Account)
        {
            if (Account.Id != 0)
            {

                Account.Password = "***************";

                var AccountUpdate = new AccountViewModel()
                {
                    Email = Account.Email,
                    Name = Account.Name,
                    Password = Account.Password,
                    Id = Account.Id
                };

                return PartialView("~/Views/Account/Partial/AccountsAddPartial.cshtml", AccountUpdate);
            }
            else
            {
                var model = new AccountViewModel()
                {

                };
                return PartialView("~/Views/Account/Partial/AccountsAddPartial.cshtml", model);
            }
        }

        /// <summary>
        /// Updates a Account with the specified details.
        /// </summary>
        /// <param name="Account">AccountUpdateDto</param>
        /// <returns>Redirects to the Account index.</returns>
        public IActionResult UpdateAccount(AccountUpdateDto Account)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpAccount);

            var Accounts = baseApi.PutToApi($"Account/{Account.Id}", Account, token);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Creates a new Account with the specified details.
        /// </summary>
        /// <param name="Account">AccountDto.</param>
        /// <returns>Redirects to the Account index.</returns>
        public IActionResult CreateAccount(AccountDto Account)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpAccount);
            var usuarios = baseApi.PostToApi("Accounts/register", Account, token);

            return RedirectToAction("Index");
        }
    }
}
