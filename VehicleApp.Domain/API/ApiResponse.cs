using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp.Domain.API
{
    public class ApiResponse<T>
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<T> Results { get; set; }
        public bool IsSuccess => !string.IsNullOrWhiteSpace(Message) && Results != null;

    }
}
