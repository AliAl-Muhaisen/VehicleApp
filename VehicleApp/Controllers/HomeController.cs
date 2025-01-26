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
using VehicleApp.Helper;
using VehicleApp.Infrastructure.Services;

namespace VehicleApp.Controllers
{

    /// <summary>
    /// The HomeController handles requests related to vehicle data, such as makes, models, and types, 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleApiService _vehicleApiService;
        public HomeController(ILogger<HomeController> logger, IVehicleApiService vehicleApiService)
        {
            _logger = logger;
            _vehicleApiService = vehicleApiService;
        }

        /// <summary>
        /// Handles requests to the home page and retrieves a list of vehicle makes to display.
        /// </summary>
        /// <returns>The Index view with a list of vehicle makes or an error message if the data could not be loaded.</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var makesResponse = await _vehicleApiService.GetMakesAsync();
                if (makesResponse == null || !makesResponse.IsSuccess)
                {
                    ViewBag.ErrorMessage = "Something went wrong, Please try again";
                    return View(new List<Make>());
                }

                return View(makesResponse.Results);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("Custom", "Error", new ErrorModel{ Title = "Faild To Load Data", Message = "The requested resource was not found." });
            }

        }
        /// <summary>
        /// Retrieves the models of a specific make and year to display their details.
        /// </summary>
        /// <param name="makeId">The ID of the selected vehicle make.</param>
        /// <param name="selectedYear">The year of the vehicle models to retrieve.</param>
        /// <returns>The ModelDetails view with a list of vehicle models or an error message if data could not be loaded.</returns>
        public async Task<IActionResult> ModelDetails(long makeId, int selectedYear)
        {
            try
            {
                var modelResponse = await _vehicleApiService.GetMakeModelsAsync(makeId, selectedYear);

                if (modelResponse == null || !modelResponse.IsSuccess)
                {
                    return RedirectToAction("Custom", "Error", new ErrorModel { Title = "Faild To Load Data", Message = "Something went wrong, Please try again" });
                }
                return View(modelResponse.Results);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("Custom", "Error", new ErrorModel { Title = "Faild To Load Data", Message = "The requested resource was not found." });
            }
        }

        /// <summary>
        /// Retrieves the types of vehicles for a specific make to display their details.
        /// </summary>
        /// <param name="makeId">The ID of the selected vehicle make.</param>
        /// <returns>The VehicleTypes view with a list of vehicle types or an error message if data could not be loaded.</returns>
        public async Task<IActionResult> VehicleTypes(long makeId)
        {
            try
            {
                var typesResponse = await _vehicleApiService.GetMakeVehicleTypeAsync(makeId);

                if (typesResponse == null || !typesResponse.IsSuccess)
                {
                    return RedirectToAction("Custom", "Error", new ErrorModel{ Title = "Faild To Load Data", Message = "Something went wrong, Please try again" });
                }
                return View(typesResponse.Results);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("Custom", "Error", new ErrorModel { Title = "Faild To Load Data", Message = "The requested resource was not found." });
            }
        }
        public IActionResult _YearSelectionDialog()
        {
            try
            {
                return PartialView("_YearSelectionDialog");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("Custom", "Error", new ErrorModel { Title = "Faild To Load Data", Message = "The requested resource was not found." });
            }
        }
        public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("Custom", "Error", new ErrorModel { Title = "Faild To Load Data", Message = "The requested resource was not found." });
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
