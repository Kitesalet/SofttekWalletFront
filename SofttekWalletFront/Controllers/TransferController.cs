using Data.Base;
using Data.DTO.Transfer;
using Microsoft.AspNetCore.Mvc;

namespace SofttekWalletFront.Controllers
{
    public class TransferController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        /// <summary>
        /// Initializes a new instance of the TransferController class.
        /// </summary>
        /// <param name="httpClient">IHttpAccountFactory</param>
        public TransferController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BeginTransaction(TransferDto dto)
        {

            string token = HttpContext.Session.GetString("Token");


            BaseApi api = new BaseApi(_httpClient);

            var transfer = api.PutToApi("account/transfer", dto, token);

            return RedirectToAction("Index");

        }
    }
}
