using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using wreq.Models.ViewModels;
using System.Net;
using Microsoft.AspNet.Identity;
using wreq.Models.Entities;

namespace wreq.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        ApplicationUserManager _userManager;

        protected IMapper _mapper;

        public UsersController(ApplicationUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        ActionResult ShowRecord(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser record = _userManager.FindById(id);

            if (record == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(_mapper.Map<ApplicationUserViewModel>(record));
        }

        public ActionResult Index()
        {
            List<ApplicationUser> records = _userManager.Users.ToList();

            return View(_mapper.Map<IEnumerable<ApplicationUserViewModel>>(records));
        }

        public ActionResult Details(string id)
        {
            return ShowRecord(id);
        }

        public ActionResult Edit(string id)
        {
            return ShowRecord(id);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUserViewModel recordViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser record = _userManager.FindById(recordViewModel.Id);
                    record.UserName = recordViewModel.UserName;
                    record.Email = recordViewModel.Email;

                    _userManager.Update(record);
                    return RedirectToAction("Details", new { id = recordViewModel.Id });
                }
            }
            catch
            {
                return View();
            }
            return View();
        }       

        public ActionResult Delete(string id)
        {
            return ShowRecord(id);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(string id)
        {
            ApplicationUser record = _userManager.FindById(id);
            
            if(User.Identity.GetUserId() == id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (record == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            try
            {
                _userManager.Delete(record);
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