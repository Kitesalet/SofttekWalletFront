using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Transfer
{
    public class TransferDto
    {
        public decimal Amount { get; set; }
        public int DestinationAccountId { get; set; }
        public int OriginAccountId { get; set; }
        public string Concept { get; set; }

    }
}
