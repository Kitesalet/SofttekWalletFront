using BilleteraVirtualSofttekBack.Models.DTOs.Client;
using Data.Base;
using Data.DTO;
using Data.DTO.Client;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace IntegradorSofttekImanolFront.Controllers
{
    [Authorize]
    /// <summary>
    /// Represents a controller for managing clients.
    /// </summary>
    public class ClientController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        /// <summary>
        /// Initializes a new instance of the clientController class.
        /// </summary>
        /// <param name="httpClient">IHttpClientFactory</param>
        public ClientController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Displays the client index view.
        /// </summary>
        /// <returns>The client index view.</returns>
        public async Task<IActionResult> Index()
        {

            var token = HttpContext.Session.GetString("Token");
            var id = HttpContext.Session.GetString("Client");
            var baseApi = new BaseApi(_httpClient);

            var response = await baseApi.GetToApi($"client/{id}", new {id = id }, token);

            var objectResponse = response as ObjectResult;

            ApiClientResponse responseDeserialized = JsonConvert.DeserializeObject<ApiClientResponse>(objectResponse.Value.ToString());

            ClientViewModel model = new ClientViewModel()
            {
                Email = responseDeserialized.Data.Email,
                Id = responseDeserialized.Data.Id,
                Name = responseDeserialized.Data.Name
            };

            return View(model);
        }

        /// <summary>
        /// Deletes a client with the specified code.
        /// </summary>
        /// <param name="codclient">An int.</param>
        /// <returns>Redirects to the client index.</returns>
        public IActionResult DeleteClient(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            var usuarios = baseApi.DeleteToApi($"client/{id}", new { id = id}, token);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays a partial view for adding or updating a client.
        /// </summary>
        /// <param name="client">ClientDto</param>
        /// <returns>A partial view for adding or updating a client.</returns>
        public IActionResult ClientsAddPartial(ClientDto client)
        {
            if (client.Id != 0)
            {

                client.Password = "***************";

                var clientUpdate = new ClientViewModel()
                {
                    Email = client.Email,
                    Name = client.Name,
                    Password = client.Password,
                    Id = client.Id
                };

                return PartialView("~/Views/Client/Partial/ClientsAddPartial.cshtml", clientUpdate);
            }
            else
            {
                var model = new ClientViewModel()
                {
                   
                };
                return PartialView("~/Views/Client/Partial/ClientsAddPartial.cshtml", model);
            }
        }

        /// <summary>
        /// Updates a client with the specified details.
        /// </summary>
        /// <param name="client">clientUpdateDto</param>
        /// <returns>Redirects to the client index.</returns>
        public IActionResult Updateclient(ClientUpdateDto client)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            var clients = baseApi.PutToApi($"client/{client.Id}", client, token);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Creates a new client with the specified details.
        /// </summary>
        /// <param name="client">ClientDto.</param>
        /// <returns>Redirects to the client index.</returns>
        public IActionResult CreateClient(ClientDto client)
        {
            client.Role = ClientRole.Base;
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var usuarios = baseApi.PostToApi("clients/register", client, token);

            return RedirectToAction("Index");
        }
    }
}
