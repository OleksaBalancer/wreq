using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wreq.Models.ViewModels
{
    public class IrrigationsIndexViewModel
    {
        public int CropId { get; set; }
        public IEnumerable<IrrigationViewModel> Irrigations { get; set; }
    }
}