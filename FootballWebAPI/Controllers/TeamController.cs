using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    public class TeamController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
