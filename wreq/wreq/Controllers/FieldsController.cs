using System.Collections.Generic;
using System.Web.Mvc;
using wreq.Models.Entities;
using System.Net;
using AutoMapper;
using wreq.Models.ViewModels;
using Microsoft.AspNet.Identity;
using wreq.Controllers.Abstract;
using wreq.DAL.Abstract;

namespace wreq.Controllers
{
    [Authorize]
    public class FieldsController : CRUDControllerWithPermissions<Field, FieldViewModel, FieldListViewModel>
    {
        public FieldsController(IDataService dataService, IMapper mapper) : base(dataService, mapper)
        {
        }

        override public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Field field = _dataService.GetByID<Field>(id.Value);

            if (field == null)
            {
                return HttpNotFound();
            }

            if (!CheckPermission(field.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            FieldViewModel fieldViewModel = _mapper.Map<FieldViewModel>(field);
            fieldViewModel.SoilTypes = _mapper.Map<IEnumerable<SoilTypeListViewModel>>(_dataService.Get<SoilType>());
            fieldViewModel.IrrigationTypes = _mapper.Map<IEnumerable<IrrigationTypeListViewModel>>(_dataService.Get<IrrigationType>());

            return View(fieldViewModel);
        }

        [HttpPost]
        override public ActionResult Edit(FieldViewModel fieldViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Field field = _mapper.Map<Field>(fieldViewModel);
                    if (CheckPermission(field.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User))
                    {
                        field.AuthorId = User.Identity.GetUserId();
                        _dataService.Update<Field>(field);
                        _dataService.Save();
                        return RedirectToAction("Details", new { id = fieldViewModel.Id });
                    }
                }
            }
            catch
            {
                return View();
            }
            fieldViewModel.SoilTypes = _mapper.Map<IEnumerable<SoilTypeListViewModel>>(_dataService.Get<SoilType>());
            fieldViewModel.IrrigationTypes = _mapper.Map<IEnumerable<IrrigationTypeListViewModel>>(_dataService.Get<IrrigationType>());
            return View(fieldViewModel);
        }

        override public ActionResult Create()
        {
            return View(new FieldViewModel()
            {
                SoilTypes = _mapper.Map<IEnumerable<SoilTypeListViewModel>>(_dataService.Get<SoilType>()),
                IrrigationTypes = _mapper.Map<IEnumerable<IrrigationTypeListViewModel>>(_dataService.Get<IrrigationType>()),
                SoilTypeId = 0,
                IrrigationTypeId = 0
            });
        }
        
    }
}