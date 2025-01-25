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

namespace VehicleApp.Infrastructure.Services
{
    public class VehicleApiService: IVehicleApiService
    {
        private readonly HttpClient _httpClient;

        public VehicleApiService(HttpClient pHttpClient)
        {
            try
            {
                _httpClient = pHttpClient;
            }
            catch (Exception)
            {
               
            }
        }

       
        public async Task<ApiResponse<Make>> GetMakesAsync()
        {
            try
            {
                var tResponse =await _httpClient.GetAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");
                tResponse.EnsureSuccessStatusCode();
                var tContent = await tResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ApiResponse<Make>>(tContent);

            }
            catch (Exception)
            {
                return null;
            }
        }

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
            catch (Exception)
            {
                return null;
            }
        }

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
            catch (Exception)
            {
                return null;
            }

        }
    }
}
