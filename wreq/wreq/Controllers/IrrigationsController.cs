using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using wreq.BL;
using wreq.BL.Abstract;
using wreq.Controllers.Abstract;
using wreq.DAL.Abstract;
using wreq.Models.Entities;
using wreq.Models.ViewModels;

namespace wreq.Controllers
{
    public class IrrigationsController : ControllerWithPermissions
    {
        IDataService _dataService;
        
        public IrrigationsController(IDataService dataService, ICropStateModeller planner)
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

            List<Irrigation> irrigations = crop.Irrigations.ToList();
            
            return View(new IrrigationsIndexViewModel() {
                CropId = cropId,
                Irrigations =  irrigations.Select(x => new IrrigationViewModel() {
                Id = x.Id,
                CropId = cropId,
                Date = x.Date,
                Volume = ParametersCalculator.DepthToVolume(x.Depth, crop.Field.Area, crop.Field.IrrigationType.WettedSoilAreaFraction)
                })});
        }

        public ActionResult Create(int cropId)
        {
            return View(new IrrigationViewModel() { CropId = cropId});
        }

        [HttpPost]
        public ActionResult Create(IrrigationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    Crop crop = _dataService.GetByID<Crop>(model.CropId);

                    if (!crop.Irrigations.Any(x => x.Date.Date == model.Date.Date))
                    {
                        Irrigation irrigation = new Irrigation()
                        {
                            CropId = model.CropId,
                            Date = model.Date,
                            Depth = ParametersCalculator.VolumeToDepth(model.Volume, crop.Field.Area, crop.Field.IrrigationType.WettedSoilAreaFraction)
                        };
                        _dataService.Insert<Irrigation>(irrigation);
                        _dataService.Save();
                        return RedirectToAction("Details", "Crops", new { id = model.CropId });
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, Resource.IrrigationValidationError);
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

            Irrigation record = _dataService.GetByID<Irrigation>(id.Value);

            if (record == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (!CheckPermission(record.Crop.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(new IrrigationViewModel()
            {
                Id = record.Id,
                CropId = record.CropId,
                Date = record.Date,
                Volume = ParametersCalculator.DepthToVolume(record.Depth, record.Crop.Field.Area, record.Crop.Field.IrrigationType.WettedSoilAreaFraction)
            });
        }

        [HttpPost]
        public ActionResult Delete(IrrigationViewModel model)
        {
            Irrigation record = _dataService.GetByID<Irrigation>(model.Id);

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
                _dataService.Delete<Irrigation>(model.Id);
                _dataService.Save();
                return RedirectToAction("Index", new { CropId = model.CropId }); 
            }
            catch
            {
                return View(model);
            }
        }
    }
}