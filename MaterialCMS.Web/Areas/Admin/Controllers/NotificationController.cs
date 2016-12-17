﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MaterialCMS.Entities.Notifications;
using MaterialCMS.Services.Notifications;
using MaterialCMS.Web.Areas.Admin.ACL;
using MaterialCMS.Web.Areas.Admin.Models;
using MaterialCMS.Web.Areas.Admin.Services;
using MaterialCMS.Website;
using MaterialCMS.Website.Controllers;

namespace MaterialCMS.Web.Areas.Admin.Controllers
{
    public class NotificationController : MaterialCMSAdminController
    {
        private readonly INotificationAdminService _service;

        public NotificationController(INotificationAdminService service)
        {
            _service = service;
        }

        public ViewResult Index(NotificationSearchQuery searchQuery)
        {
            ViewData["results"] = _service.Search(searchQuery);
            ViewData["notification-type-options"] = _service.GetNotificationTypeOptions(true);
            return View(searchQuery);
        }

        [HttpGet]
        public ViewResult Push()
        {
            ViewData["publish-type-options"] = _service.GetPublishTypeOptions();
            ViewData["notification-type-options"] = _service.GetNotificationTypeOptions();
            return View(new PushNotificationModel());
        }

        [HttpPost]
        public RedirectToRouteResult Push(PushNotificationModel model)
        {
            _service.PushNotification(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [MaterialCMSACLRule(typeof(NotificationACL), NotificationACL.Delete)]
        public ViewResult Delete(Notification notification)
        {
            return View(notification);
        }

        [HttpPost]
        [ActionName("Delete")]
        [MaterialCMSACLRule(typeof(NotificationACL), NotificationACL.Delete)]
        public RedirectToRouteResult Delete_POST(Notification notification)
        {
            _service.Delete(notification);
            return RedirectToAction("Index");
        }
    }

    public class PushNotificationModel
    {
        [Required]
        public string Message { get; set; }
        [DisplayName("Publish Type")]
        public PublishType PublishType { get; set; }
        [DisplayName("Notification Type")]
        public NotificationType NotificationType { get; set; }
    }
}