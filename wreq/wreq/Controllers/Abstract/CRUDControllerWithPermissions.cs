using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using wreq.DAL.Abstract;
using wreq.Models.Abstract;
using wreq.Models.ViewModels;

namespace wreq.Controllers.Abstract
{
    abstract public class CRUDControllerWithPermissions<TEntity, TEntityViewModel, TEntityListViewModel> : ControllerWithPermissions
        where TEntity : class, IHasAuthorAndName
        where TEntityViewModel : class, IHasId
        where TEntityListViewModel : class
    {
        protected IDataService _dataService;

        protected IMapper _mapper;

        public CRUDControllerWithPermissions(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        virtual protected ActionResult ShowRecord(int? id, Permissions permissions)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TEntity record = _dataService.GetByID<TEntity>(id.Value);

            if (record == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (!CheckPermission(record.AuthorId, permissions, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(_mapper.Map<TEntityViewModel>(record));
        }

        virtual public ActionResult Index()
        {
            List<TEntity> records;
            if (User.IsInRole("Admin"))
            {
                records = _dataService.Get<TEntity>().OrderBy(x => x.Name).ToList();
            }
            else
            {
                List<TEntity> recordsDefault = _dataService.GetByAuthor<TEntity>(null).OrderBy(x => x.Name).ToList();
                List<TEntity> recordsCustom = _dataService.GetByAuthor<TEntity>(User.Identity.GetUserId()).OrderBy(x => x.Name).ToList();
                records = recordsDefault.Concat(recordsCustom).ToList();
            }

            return View(_mapper.Map<IEnumerable<TEntityListViewModel>>(records));
        }

        virtual public ActionResult Details(int? id)
        {
            return ShowRecord(id, Permissions.DefaultRecord | Permissions.AuthoredByUser | Permissions.UserIsAdmin);
        }

        virtual public ActionResult Edit(int? id)
        {
            return ShowRecord(id, Permissions.AuthoredByUser | Permissions.UserIsAdmin);
        }

        [HttpPost]
        virtual public ActionResult Edit(TEntityViewModel recordViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TEntity record = _mapper.Map<TEntity>(recordViewModel);
                    if (CheckPermission(record.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User))
                    {
                        _dataService.Update<TEntity>(record);
                        _dataService.Save();
                        return RedirectToAction("Details", new { id = recordViewModel.Id });
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        virtual public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        virtual public ActionResult Create(TEntityViewModel recordViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TEntity record = _mapper.Map<TEntity>(recordViewModel);
                    if (User.IsInRole("Admin"))
                    {
                        record.AuthorId = null;
                    }
                    else
                    {
                        record.AuthorId = User.Identity.GetUserId();
                    }
                    _dataService.Insert<TEntity>(record);
                    _dataService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        virtual public ActionResult Delete(int? id)
        {
            return ShowRecord(id, Permissions.AuthoredByUser | Permissions.UserIsAdmin);
        }

        [HttpPost, ActionName("Delete")]
        virtual public ActionResult DeleteConfirm(int id)
        {
            TEntity record = _dataService.GetByID<TEntity>(id);

            if (record == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (!(CheckPermission(record.AuthorId, Permissions.AuthoredByUser | Permissions.UserIsAdmin, User)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            try
            {
                _dataService.Delete<TEntity>(id);
                _dataService.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Resource.RecordUsed);
                return View();
            }
        }
    }
}