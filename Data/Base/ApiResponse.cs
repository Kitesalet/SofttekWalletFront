using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    /// <summary>
    /// Represents an API response.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// The data included in the response.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The HTTP status code associated with the response.
        /// </summary>
        public int StatusCode { get; set; }
    }
}
