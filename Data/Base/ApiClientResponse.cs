using Data.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    /// <summary>
    /// Represents an API response containing data for a client.
    /// </summary>
    public class ApiClientResponse
    {
        /// <summary>
        /// Gets or sets the client data.
        /// </summary>
        public ClientDto Data { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code associated with the response.
        /// </summary>
        public int StatusCode { get; set; }
    }

}
