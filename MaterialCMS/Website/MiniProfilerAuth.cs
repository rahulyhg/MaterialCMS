﻿using System.Linq;
using System.Web;
using MaterialCMS.Entities.People;
using MaterialCMS.Services;
using MaterialCMS.Website.Profiling;

namespace MaterialCMS.Website
{
    public static class MiniProfilerAuth
    {
        public static bool IsUserAllowedToSeeMiniProfilerUI(HttpRequest arg)
        {
            var userService = arg.RequestContext.HttpContext.Get<IUserLookup>();
            if (userService == null)
                return false;
            User currentUser = userService.GetCurrentUser(arg.RequestContext.HttpContext);
            return currentUser != null && currentUser.IsAdmin;
        }

        public static bool ShouldStartFor(HttpRequest request)
        {
            var miniProfilerEnabled = CurrentRequestData.SiteSettings != null && CurrentRequestData.SiteSettings.MiniProfilerEnabled;
            if (!miniProfilerEnabled)
                return false;

            if (CurrentRequestData.CurrentContext.GetAll<IReasonToEnableMiniProfiler>().Any(reason => reason.ShouldEnableFor(request)))
                return true;

            return !CurrentRequestData.CurrentContext.GetAll<IReasonToDisableMiniProfiler>().Any(reason => reason.ShouldDisableFor(request));
        }
    }
}