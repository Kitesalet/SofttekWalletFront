using Data.Base;
using Data.DTO.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace SofttekWalletFront.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Starts the money transfer between two accounts.
        /// </summary>
        /// <param name="dto">A TransferDto instantiation that has the information about both accounts, as well as the balance to transfer.</param>
        /// <returns>An IActionResult object with the text information regarding the HTTP request.</returns>
        public async Task<IActionResult> BeginTransaction(TransferDto dto)
        {

            string token = HttpContext.Session.GetString("Token");


            BaseApi api = new BaseApi(_httpClient);

            var response = await api.PutToApi("account/transfer", dto, token);

            var objectResponse = response as ObjectResult;

            if(objectResponse.StatusCode == (int)HttpStatusCode.OK)
            {

                var jsonObject = JsonConvert.DeserializeObject<ApiResponse>(objectResponse.Value.ToString());
                return Ok(jsonObject.Data);
            }
            else
            {
                var jsonObject = JsonConvert.DeserializeObject<ApiErrorResponse>(objectResponse.Value.ToString());
                return BadRequest(jsonObject.Errors[0].Error);
            }


        }
    }
}
