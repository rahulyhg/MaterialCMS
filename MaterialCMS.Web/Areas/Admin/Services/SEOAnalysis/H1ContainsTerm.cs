﻿using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MaterialCMS.Entities.Documents.Web;
using MaterialCMS.Helpers;
using MaterialCMS.Web.Areas.Admin.Controllers;
using MaterialCMS.Web.Areas.Admin.Helpers;
using MaterialCMS.Web.Areas.Admin.Models.SEOAnalysis;

namespace MaterialCMS.Web.Areas.Admin.Services.SEOAnalysis
{
    public class H1ContainsTerm : BaseSEOAnalysisFacetProvider
    {
        public override IEnumerable<SEOAnalysisFacet> GetFacets(Webpage webpage, HtmlNode document, string analysisTerm)
        {
            var element = document.GetElement("h1");
            if (element == null)
            {
                yield return GetFacet("H1 is missing", SEOAnalysisStatus.Error, "The H1 element is missing in the page");
                yield break;
            }
            string h1Text = document.GetElementText("h1");
            if (h1Text.Contains(analysisTerm, StringComparison.OrdinalIgnoreCase))
                yield return
                    GetFacet("H1 contains term", SEOAnalysisStatus.Success,
                        "The H1 element contains '" + analysisTerm + "'");
            else
                yield return
                    GetFacet("H1 contains term", SEOAnalysisStatus.Error,
                        "The H1 element does not contain '" + analysisTerm + "'");
        }
    }
}