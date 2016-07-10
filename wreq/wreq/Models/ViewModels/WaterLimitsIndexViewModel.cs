using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wreq.Models.ViewModels
{
    public class WaterLimitsIndexViewModel
    {
        public int CropId { get; set; }
        public IEnumerable<WaterLimitViewModel> WaterLimits { get; set; }
    }
}