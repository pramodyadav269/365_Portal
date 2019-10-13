using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal.Models
{
    public class RequestBase
    {
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string RequesString { get; set; }
        public string ResponseString { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public string ClienIPAddress { get; set; }
        public string IMEI { get; set; }
        public string DeviceModelNo { get; set; }
        public string DeviceType { get; set; }
        public string AppWebVersion { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
    }
}