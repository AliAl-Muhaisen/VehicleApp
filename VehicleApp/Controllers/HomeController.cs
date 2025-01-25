using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleApp.Domain.Interface;
using VehicleApp.Domain.Shared;
using VehicleApp.Domain.Vehicle;
using VehicleApp.Infrastructure.Services;

namespace VehicleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleApiService _vehicleApiService;
        public HomeController(ILogger<HomeController> logger, IVehicleApiService vehicleApiService)
        {
            _logger = logger;
            _vehicleApiService = vehicleApiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var makesResponse = await _vehicleApiService.GetMakesAsync();
                if (makesResponse == null || !makesResponse.IsSuccess)
                {
                    // Handle error (optional)
                    ViewBag.ErrorMessage = "Something went wrong, Please try again";
                    return View(new List<Make>());
                }

                return View(makesResponse.Results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Custom", "Error", new ErrorModel{ Title = "Faild To Load Data", Message = "The requested resource was not found." });
            }

        }
        public async Task<IActionResult> ModelDetails(long makeId, int selectedYear)
        {
            try
            {
                var modelResponse = await _vehicleApiService.GetMakeModelsAsync(makeId, selectedYear);

                if (modelResponse == null || !modelResponse.IsSuccess)
                {
                    // Handle error (optional)
                    ViewBag.ErrorMessage = "Something went wrong, Please try again";
                    TempData["ErrorMessage"] = modelResponse.Message;
                    return View(new List<VehicleModel>());
                }
                return View(modelResponse.Results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Index", "Error", new { title = "Faild To Load Data", message = "The requested resource was not found." });
            }
        }
        public async Task<IActionResult> VehicleTypes(long makeId)
        {
            try
            {
                var typesResponse = await _vehicleApiService.GetMakeVehicleTypeAsync(makeId);

                if (typesResponse == null || !typesResponse.IsSuccess)
                {
                    // Handle error (optional)
                    ViewBag.ErrorMessage = "Something went wrong, Please try again";
                    TempData["ErrorMessage"] = typesResponse.Message;
                    return View(new List<VehicleModel>());
                }
                return View(typesResponse.Results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Index", "Error", new { title = "Faild To Load Data", message = "The requested resource was not found." });
            }
        }
        public IActionResult _YearSelectionDialog()
        {
            try
            {
                return PartialView("_YearSelectionDialog");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error", new { title = "Faild To Load Componenet", message = "Something went wrong please try again later" });
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
