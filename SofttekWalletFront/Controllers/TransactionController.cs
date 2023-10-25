using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SofttekWalletFront.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



    }
}
