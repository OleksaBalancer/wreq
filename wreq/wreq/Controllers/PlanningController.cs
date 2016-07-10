using AutoMapper;
using System;
using System.Collections;
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
    public class PlanningController : ControllerWithPermissions
    {
        IDataService _dataService;

        IMapper _mapper;

        ICropStateModeller _planner;


        public PlanningController(IDataService dataService, IMapper mapper, ICropStateModeller planner)
        {
            _dataService = dataService;
            _mapper = mapper;
            _planner = planner;
        }

        public ActionResult CropPlan(int? cropId)
        {
            if (cropId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Crop crop = _dataService.GetByID<Crop>(cropId.Value);

            if (crop == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (!CheckPermission(crop.AuthorId, Permissions.AuthoredByUser | Permissions.DefaultRecord | Permissions.UserIsAdmin, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            //List<CropStateRecord> plannedIrrigations = crop.CropStateRecords.Where(x => x.PlannedIrrigation != 0).ToList();

            PlanningViewModel model = new PlanningViewModel()
            {
                CropId = crop.Id,
                CropName = crop.Name,
                DateSeeded = crop.DateSeeded,
                LengthIni = crop.LengthIni,
                LengthDev = crop.LengthDev,
                LengthMid = crop.LengthMid,
                LengthLate = crop.LengthLate
            };

            //if (plannedIrrigations.Count > 0)
            //{

            //    model.DateBegin = plannedIrrigations.Select(x => x.Date).Min();
            //    model.DateEnd = plannedIrrigations.Select(x => x.Date).Max();
            //    model.Irrigations = plannedIrrigations.Select(x => new IrrigationViewModel() { Date = x.Date, Volume = ParametersCalculator.DepthToVolume(x.PlannedIrrigation, crop.Field.Area, crop.Field.IrrigationType.WettedSoilAreaFraction) });
            //}

            return View(model);
        }

        [HttpPost]
        public ActionResult CropPlan(PlanningViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    Crop crop = _dataService.GetByID<Crop>(model.CropId);

                    if (crop != null)
                    {

                        List<CropStateRecord> calculatedPlans = _planner.ModelCropState(crop, model.DateBegin, model.DateEnd).ToList();

                        model.Irrigations = calculatedPlans.Where(x => x.PlannedIrrigation != 0).Select(
                            x => new IrrigationViewModel()
                            {
                                Date = x.Date,
                                Volume = ParametersCalculator.DepthToVolume(x.PlannedIrrigation, crop.Field.Area, crop.Field.IrrigationType.WettedSoilAreaFraction)
                            }
                            ).ToList();

                        model.YieldReduction = calculatedPlans.Sum(x => x.YieldReduction) / (crop.LengthIni + crop.LengthDev + crop.LengthMid + crop.LengthLate) * 100;

                        _dataService.Update<Crop>(crop);//save new crop state records and weather data
                        _dataService.Save();
                    }                    
                }
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError(String.Empty,ex.Message);
                return View(model);
            }

            return View(model);
        }

    }
}