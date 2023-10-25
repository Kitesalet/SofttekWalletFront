using Data.DTO.Account;
using Data.DTO.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Transaction
{
    public class TransactionGetDto
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Concept { get; set; }
        public AccountDto SourceAccount { get; set; }
        public AccountDto DestinationAccount { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
