using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    /// <summary>
    /// Represents an API error response, providing information about the error.
    /// </summary>
    public class ApiErrorResponse
    {

        /// <summary>
        /// The HTTP status code associated with the error.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The list of error details.
        /// </summary>
        public List<ResponseError>? Errors { get; set; }

        /// <summary>
        /// This class instantiates an individual error in an API error response.
        /// </summary>
        public class ResponseError
        {
            /// <summary>
            /// The error message.
            /// </summary>
            public string? Error { get; set; }

            /// <summary>
            /// The additional data.
            /// </summary>
            public string? Data { get; set; }
        }

    }
}
