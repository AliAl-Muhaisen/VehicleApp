using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp.Domain.Vehicle
{
    public class VehicleModel : Make
    {
        public int Model_ID { get; set; }
        public string Model_Name { get; set; }
    }
}
