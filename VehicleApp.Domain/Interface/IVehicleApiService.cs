using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Domain.API;
using VehicleApp.Domain.Vehicle;

namespace VehicleApp.Domain.Interface
{
    public interface IVehicleApiService
    {
        Task<ApiResponse<Make>> GetMakesAsync();
        Task<ApiResponse<VehicleModel>> GetMakeModelsAsync(long makeId, int year);
        Task<ApiResponse<VehicleType>> GetMakeVehicleTypeAsync(long makeId);
    }
}
