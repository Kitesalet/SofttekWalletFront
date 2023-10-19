using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Account
{
    public class AccountDto
    {

        public int Id { get; set; }
        public string UUID { get; set; }
        public int AccountNumber { get; set; }
        public int CBU { get; set; }
        public string? Alias { get; set; }
        public decimal Balance { get; set; }
        public int Type { get; set; }

        public int ClientId { get; set; }

    }
}
