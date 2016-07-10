using System.Collections.Generic;
using System.Web.Mvc;
using wreq.Models.Entities;
using System.Net;
using AutoMapper;
using wreq.Models.ViewModels;
using Microsoft.AspNet.Identity;
using wreq.DAL.Abstract;
using wreq.Controllers.Abstract;

namespace wreq.Controllers
{
    [Authorize]
    public class CropsController : CRUDControllerWithPermissions<Crop, CropViewModel, CropListViewModel>
    {
        public CropsController(IDataService dataService, IMapper mapper) : base(dataService, mapper)
        {
        }

        override public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Crop crop = _dataService.GetByID<Crop>(id.Value);

            if (crop == null)
            {
                return HttpNotFound();
            }

            if (!CheckPermission(crop.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            CropViewModel cropViewModel = _mapper.Map<CropViewModel>(crop);
            cropViewModel.Cultures = _mapper.Map<IEnumerable<CultureListViewModel>>(_dataService.Get<Culture>());
            cropViewModel.Fields = _mapper.Map<IEnumerable<FieldListViewModel>>(_dataService.Get<Field>());

            return View(cropViewModel);
        }

        [HttpPost]
        override public ActionResult Edit(CropViewModel cropViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Crop crop = _mapper.Map<Crop>(cropViewModel);
                    if (CheckPermission(crop.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User))
                    {
                        crop.AuthorId = User.Identity.GetUserId();
                        _dataService.Update<Crop>(crop);
                        _dataService.Save();
                        return RedirectToAction("Details", new { id = cropViewModel.Id });
                    }
                }
            }
            catch
            {
                return View();
            }
            cropViewModel.Cultures = _mapper.Map<IEnumerable<CultureListViewModel>>(_dataService.Get<Culture>());
            cropViewModel.Fields = _mapper.Map<IEnumerable<FieldListViewModel>>(_dataService.Get<Field>());
            return View(cropViewModel);
        }

        override public ActionResult Create()
        {
            return View(new CropViewModel()
            {
                Cultures = _mapper.Map<IEnumerable<CultureListViewModel>>(_dataService.Get<Culture>()),
                Fields = _mapper.Map<IEnumerable<FieldListViewModel>>(_dataService.Get<Field>()),
                CultureId = 0,
                FieldId = 0
            });
        }

        public JsonResult GetGrowthStages(int cultureId)
        {
            Culture culture = _dataService.GetByID<Culture>(cultureId);
            GrowthStagesResult result = new GrowthStagesResult();
            if (culture != null)
            {
                result.Lini = culture.LengthIni;
                result.Ldev = culture.LengthDev;
                result.Lmid = culture.LengthMid;
                result.Llate = culture.LengthLate;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

    public class GrowthStagesResult
    {
        public int Lini { get; set; }
        public int Ldev { get; set; }
        public int Lmid { get; set; }
        public int Llate { get; set; }
    }
}