using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using wreq.BL.Abstract;
using wreq.Controllers.Abstract;
using wreq.DAL.Abstract;
using wreq.Models.Entities;
using wreq.Models.ViewModels;

namespace wreq.Controllers
{
    public class WaterLimitsController : ControllerWithPermissions
    {
        IDataService _dataService;

        public WaterLimitsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public ActionResult Index(int cropId)
        {
            Crop crop = _dataService.GetByID<Crop>(cropId);

            if (!(CheckPermission(crop.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            List<WaterLimit> waterLimits = crop.WaterLimits.ToList();

            return View(new WaterLimitsIndexViewModel()
            {
                CropId = cropId,
                WaterLimits = waterLimits.Select(x => new WaterLimitViewModel()
                {
                    Id = x.Id,
                    CropId = cropId,
                    DateBegin = x.DateBegin,
                    DateEnd = x.DateEnd,
                    Volume = x.Volume
                })
            });
        }

        public ActionResult Create(int cropId)
        {
            return View(new WaterLimitViewModel() { CropId = cropId });
        }

        [HttpPost]
        public ActionResult Create(WaterLimitViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Crop crop = _dataService.GetByID<Crop>(model.CropId);
                    bool overlapping = false;
                    foreach (WaterLimit wl in crop.WaterLimits)
                    {
                        if (!(wl.DateBegin > model.DateEnd && wl.DateEnd > model.DateEnd) && !(wl.DateBegin < model.DateBegin && wl.DateEnd < model.DateBegin))
                        {
                            overlapping = true;
                            break;
                        }
                    }

                    if (!overlapping) //check if there are overlapping
                    {
                        WaterLimit waterLimit = new WaterLimit()
                        {
                            CropId = model.CropId,
                            DateBegin = model.DateBegin,
                            DateEnd = model.DateEnd,
                            Volume = model.Volume
                        };
                        _dataService.Insert<WaterLimit>(waterLimit);
                        _dataService.Save();
                        return RedirectToAction("Details", "Crops", new { id = model.CropId });
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, Resource.WaterLimitsValidationError);
                        return View(model);
                    }
                }
            }
            catch
            {
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WaterLimit record = _dataService.GetByID<WaterLimit>(id.Value);

            if (record == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (!CheckPermission(record.Crop.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(new WaterLimitViewModel()
            {
                Id = record.Id,
                DateBegin = record.DateBegin,
                DateEnd = record.DateEnd,
                Volume = record.Volume,
                CropId = record.CropId
            });
        }

        [HttpPost]
        public ActionResult Delete(WaterLimitViewModel model)
        {
            WaterLimit record = _dataService.GetByID<WaterLimit>(model.Id);

            if (record == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (!(CheckPermission(record.Crop.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            try
            {
                _dataService.Delete<WaterLimit>(model.Id);
                _dataService.Save();
                return RedirectToAction("Index", new { cropId = model.CropId });
            }
            catch
            {
                return View(model);
            }
        }
    }
}