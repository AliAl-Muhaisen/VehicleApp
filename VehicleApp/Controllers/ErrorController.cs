using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VehicleApp.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                _logger.LogError("An unhandled exception occurred.");
                return View("~/Views/Shared/Error.cshtml");

            }
            catch (System.Exception)
            {
                return null;
            }

        }
        public IActionResult NotFound()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
