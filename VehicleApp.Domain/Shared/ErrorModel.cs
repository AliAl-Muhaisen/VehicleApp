using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp.Domain.Shared
{
    public class ErrorModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
