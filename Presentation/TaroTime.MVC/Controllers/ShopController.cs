using Microsoft.AspNetCore.Mvc;

namespace TaroTime.MVC.Controllers
{
    public class ShopController : Controller
    {
        public async Task <IActionResult> Shop()
        {
            return View();
        }
        public async Task<IActionResult> ShopSingle()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut()
        {
            return View();
        }
        public async Task<IActionResult> Cart()
        {
            return View();
        }
    }
}
