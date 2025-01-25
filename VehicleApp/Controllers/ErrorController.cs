using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleApp.Domain.Shared;

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
            try
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            catch (System.Exception)
            {
                return null;
            }
           
        }
        public IActionResult Custom(ErrorModel errorModel)
        {
            try
            {
                return View("~/Views/Shared/Error.cshtml", errorModel);
            }
            catch (System.Exception)
            {
                return null;
            }

        }
    }
}
