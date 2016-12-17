﻿using System.Web.Mvc;
using MaterialCMS.Helpers;
using MaterialCMS.Messages;
using MaterialCMS.Web.Areas.Admin.Helpers;
using MaterialCMS.Web.Areas.Admin.ModelBinders;
using MaterialCMS.Web.Areas.Admin.Services;
using MaterialCMS.Website.Binders;
using MaterialCMS.Website.Controllers;

namespace MaterialCMS.Web.Areas.Admin.Controllers
{
    public class MessageTemplateController : MaterialCMSAdminController
    {
        private readonly IMessageTemplateAdminService _messageTemplateAdminService;

        public MessageTemplateController(IMessageTemplateAdminService messageTemplateAdminService)
        {
            _messageTemplateAdminService = messageTemplateAdminService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_messageTemplateAdminService.GetAllMessageTemplateTypesWithDetails());
        }

        [HttpGet]
        public ActionResult AddSiteOverride(string type)
        {
            return View(_messageTemplateAdminService.GetNewOverride(type));
        }

        [HttpPost]
        [ActionName("AddSiteOverride")]
        public ActionResult AddSiteOverride_POST(
            [IoCModelBinder(typeof(MessageTemplateOverrideModelBinder))] MessageTemplate messageTemplate)
        {
            if (messageTemplate != null)
            {
                _messageTemplateAdminService.AddOverride(messageTemplate);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteSiteOverride(string type)
        {
            return View(_messageTemplateAdminService.GetOverride(type));
        }

        [HttpPost]
        [ActionName("DeleteSiteOverride")]
        public ActionResult DeleteSiteOverride_POST(
            [IoCModelBinder(typeof(DeleteMessageTemplateOverrideModelBinder))] MessageTemplate messageTemplate)
        {
            if (messageTemplate != null)
            {
                _messageTemplateAdminService.DeleteOverride(messageTemplate);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string type)
        {
            ModelState.Clear();
            MessageTemplate template = _messageTemplateAdminService.GetTemplate(type);
            if (template != null)
            {
                return View(template);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_POST(
            [IoCModelBinder(typeof(MessageTemplateOverrideModelBinder))] MessageTemplate messageTemplate)
        {
            if (messageTemplate != null)
            {
                _messageTemplateAdminService.Save(messageTemplate);
                TempData.SuccessMessages()
                    .Add(string.Format("{0} successfully edited", messageTemplate.GetType().Name.BreakUpString()));
                return RedirectToAction("Edit", new { type = messageTemplate.GetType().FullName });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ImportLegacyTemplate(string type)
        {
            return View((object)type);
        }

        [HttpPost, ActionName("ImportLegacyTemplate")]
        public ActionResult ImportLegacyTemplate_POST(string type)
        {
            _messageTemplateAdminService.ImportLegacyTemplate(type);
            return RedirectToAction("Index");
        }
    }
}