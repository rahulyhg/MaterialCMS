﻿using System.Web.Mvc;
using MaterialCMS.ACL.Rules;
using MaterialCMS.Entities.People;
using MaterialCMS.Models;
using MaterialCMS.Services;
using MaterialCMS.Web.Areas.Admin.Helpers;
using MaterialCMS.Web.Areas.Admin.ModelBinders;
using MaterialCMS.Web.Areas.Admin.Models;
using MaterialCMS.Web.Areas.Admin.Services;
using MaterialCMS.Website;
using MaterialCMS.Website.Binders;
using MaterialCMS.Website.Controllers;

namespace MaterialCMS.Web.Areas.Admin.Controllers
{
    public class UserController : MaterialCMSAdminController
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserSearchService _userSearchService;
        private readonly IRoleService _roleService;
        private readonly IPasswordManagementService _passwordManagementService;
        private readonly IGetUserCultureOptions _getUserCultureOptions;
        private readonly IGetUserEditTabsService _getUserEditTabsService;

        public UserController(IUserManagementService userManagementService, IUserSearchService userSearchService, IRoleService roleService, IPasswordManagementService passwordManagementService, IGetUserCultureOptions getUserCultureOptions, IGetUserEditTabsService getUserEditTabsService)
        {
            _userManagementService = userManagementService;
            _userSearchService = userSearchService;
            _roleService = roleService;
            _passwordManagementService = passwordManagementService;
            _getUserCultureOptions = getUserCultureOptions;
            _getUserEditTabsService = getUserEditTabsService;
        }

        [MaterialCMSACLRule(typeof(UserACL), UserACL.View)]
        public ActionResult Index(UserSearchQuery searchQuery)
        {
            ViewData["users"] = _userSearchService.GetUsersPaged(searchQuery);
            ViewData["roles"] = _userSearchService.GetAllRoleOptions();
            return View(searchQuery);
        }

        [HttpGet]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.Add)]
        public PartialViewResult Add()
        {
            var model = new AddUserModel();
            ViewData["culture-options"] = _getUserCultureOptions.Get();
            return PartialView(model);
        }

        [HttpPost]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.Add)]
        public ActionResult Add([IoCModelBinder(typeof(AddUserModelBinder))] User user)
        {
            _userManagementService.AddUser(user);

            return RedirectToAction("Edit", new { id = user.Id });
        }

        [HttpGet]
        [ActionName("Edit")]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.Edit)]
        public ActionResult Edit_Get(User user)
        {
            ViewData["AvailableRoles"] = _roleService.GetAllRoles();
            ViewData["OnlyAdmin"] = _roleService.IsOnlyAdmin(user);
            ViewData["culture-options"] = _getUserCultureOptions.Get();

            ViewData["edit-tabs"] = _getUserEditTabsService.GetEditTabs(user);
            return user == null
                       ? (ActionResult)RedirectToAction("Index")
                       : View(user);
        }

        [HttpPost]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.Edit)]
        public ActionResult Edit([IoCModelBinder(typeof(EditUserModelBinder))] User user)
        {
            _userManagementService.SaveUser(user);
            TempData.SuccessMessages().Add(string.Format("{0} successfully saved", user.Name));
            return RedirectToAction("Edit", "User", new { Id = user.Id });
        }

        [HttpGet]
        [ActionName("Delete")]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.Delete)]
        public PartialViewResult Delete_Get(User user)
        {
            return PartialView(user);
        }

        [HttpPost]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.Delete)]
        public RedirectToRouteResult Delete(User user)
        {
            _userManagementService.DeleteUser(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.SetPassword)]
        public ActionResult SetPassword(User user)
        {
            return PartialView(user);
        }

        [HttpPost]
        [MaterialCMSACLRule(typeof(UserACL), UserACL.SetPassword)]
        public ActionResult SetPassword(User user, string password)
        {
            _passwordManagementService.SetPassword(user, password, password);
            _userManagementService.SaveUser(user);
            return RedirectToAction("Edit", new { user.Id });
        }

        public JsonResult IsUniqueEmail(string email, int? id)
        {
            if (_userManagementService.IsUniqueEmail(email, id))
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json("Email already registered.", JsonRequestBehavior.AllowGet);
        }
    }
}