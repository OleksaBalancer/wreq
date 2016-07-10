using System;
using System.Collections.Generic;
using wreq.Models.Entities;

namespace wreq.BL.Abstract
{
    public interface ICropStateModeller
    {
        IEnumerable<CropStateRecord> ModelCropState(Crop crop,DateTime dateBegin, DateTime dateEnd);        
    }
}
