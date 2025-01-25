using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp.Domain.Vehicle
{
    public interface IVehicle
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
