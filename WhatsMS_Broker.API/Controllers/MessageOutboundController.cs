using Microsoft.AspNetCore.Mvc;

namespace WhatsMS_Broker.API.Controllers
{
    public class MessageOutboundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
