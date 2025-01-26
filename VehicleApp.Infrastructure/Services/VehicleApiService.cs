using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VehicleApp.Domain.API;
using VehicleApp.Domain.Interface;
using VehicleApp.Domain.Vehicle;
using VehicleApp.Helper;

namespace VehicleApp.Infrastructure.Services
{
    /// <summary>
    /// Service class to interact with the Vehicle API. 
    /// Provides methods to retrieve vehicle makes, models, and types.
    /// </summary>
    public class VehicleApiService: IVehicleApiService
    {
        private readonly HttpClient _httpClient;

      
        public VehicleApiService(HttpClient pHttpClient)
        {
            try
            {
                _httpClient = pHttpClient;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        /// <summary>
        /// Retrieves a list of all vehicle makes.
        /// </summary>
        /// <returns>An <see cref="ApiResponse{Make}"/> containing the list of makes, or null if an error occurs.</returns>

        public async Task<ApiResponse<Make>> GetMakesAsync()
        {
            try
            {
                var tResponse =await _httpClient.GetAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");
                tResponse.EnsureSuccessStatusCode();
                var tContent = await tResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ApiResponse<Make>>(tContent);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a list of models for a specific vehicle make and year.
        /// </summary>
        /// <param name="makeId">The ID of the vehicle make.</param>
        /// <param name="year">The year of the vehicle models.</param>
        /// <returns>An <see cref="ApiResponse{VehicleModel}"/> containing the list of models, or null if an error occurs.</returns>
        public async Task<ApiResponse<VehicleModel>> GetMakeModelsAsync(long makeId, int year)
        {
      
            try
            {
                //var tResponse = await _httpClient.GetFromJsonAsync<ApiResponse<VehicleModel>>($"GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json");
                var tResponse = await _httpClient.GetAsync($"GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json");
                tResponse.EnsureSuccessStatusCode();
                var tContent = await tResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<VehicleModel>>(tContent);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves the types of vehicles for a specific vehicle make.
        /// </summary>
        /// <param name="makeId">The ID of the vehicle make.</param>
        /// <returns>An <see cref="ApiResponse{VehicleType}"/> containing the list of vehicle types, or null if an error occurs.</returns>

        public async Task<ApiResponse<VehicleType>> GetMakeVehicleTypeAsync(long makeId)
        {
            try
            {
                //var response = await _httpClient.GetFromJsonAsync<ApiResponse<VehicleType>>($"GetVehicleTypesForMakeId/{makeId}?format=json");
                var tResponse = await _httpClient.GetAsync($"GetVehicleTypesForMakeId/{makeId}?format=json");
                tResponse.EnsureSuccessStatusCode();
                var tContent = await tResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<VehicleType>>(tContent);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }

        }
    }
}
