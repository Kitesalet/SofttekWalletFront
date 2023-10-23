using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    public class BaseApi : ControllerBase
    {

        private readonly IHttpClientFactory _httpClient;


        public BaseApi(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> DeleteToApi(string controllerName, object model, string token = "")
        {

            try
            {
                var client = _httpClient.CreateClient("useApi");

                if (token != "")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                }

                var response = await client.DeleteAsync(controllerName);
                var content = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    return Ok(content);
                }


                return BadRequest(content);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        public async Task<IActionResult> PutToApi(string controllerName, object model, string token = "")
        {

            try
            {
                var client = _httpClient.CreateClient("useApi");

                if (token != "")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                }

                var response = await client.PutAsJsonAsync(controllerName, model);
                var content = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    return Ok(content);
                }

                return BadRequest(content);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        public async Task<IActionResult> PostToApi(string controllerName, object model, string token = "")
        {

            try
            {
                var client = _httpClient.CreateClient("useApi");

                if (token != "")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                }

                var response = await client.PostAsJsonAsync(controllerName, model);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Ok(content);
                }

                return BadRequest(content);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

    }
}
