using System.Web.Mvc;
using wreq.Models.Entities;
using AutoMapper;
using wreq.Models.ViewModels;
using wreq.Controllers.Abstract;
using wreq.DAL.Abstract;

namespace wreq.Controllers
{
    [Authorize]
    public class SoilTypesController : CRUDControllerWithPermissions<SoilType, SoilTypeViewModel, SoilTypeListViewModel>
    {
        public SoilTypesController(IDataService dataService, IMapper mapper) : base(dataService, mapper)
        {
        }
    }
}