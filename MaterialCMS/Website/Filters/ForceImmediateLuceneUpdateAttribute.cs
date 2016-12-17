﻿using System.Web.Mvc;
using MaterialCMS.Services;
using Ninject;

namespace MaterialCMS.Website.Filters
{
    public class ForceImmediateLuceneUpdateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // only do this with local file system due to unpredictability of remote file storage
            if (filterContext.HttpContext.Get<IFileSystem>() is FileSystem)
                CurrentRequestData.OnEndRequest.Add(new ExecuteLuceneTasks());
        }
    }
}