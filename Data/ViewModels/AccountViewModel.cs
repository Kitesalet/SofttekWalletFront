using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    /// <summary>
    /// The ViewModel for account objects.
    /// </summary>
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string UUID { get; set; }
        public int AccountNumber { get; set; }
        public int CBU { get; set; }
        public string? Alias { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public int ClientId { get; set; }

    }
}
