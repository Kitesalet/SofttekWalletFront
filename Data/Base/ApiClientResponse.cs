using Data.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    public class ApiClientResponse
    {
        public ClientDto Data {get;set;}

        public int StatusCode { get; set; }
    }

}
