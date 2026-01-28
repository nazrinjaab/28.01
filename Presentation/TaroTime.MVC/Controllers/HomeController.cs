using Microsoft.AspNetCore.Mvc;
using RestSharp;


namespace TaroTime.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestClient _client;
        public HomeController()
        {
            _client = new RestClient("https://localhost:7194/");
        }
        public async Task <IActionResult> Index()
        {
            RestRequest request = new RestRequest();
            return View();
        }
        
    }
}
