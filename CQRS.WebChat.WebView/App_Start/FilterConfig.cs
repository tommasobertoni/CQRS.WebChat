﻿using System.Web;
using System.Web.Mvc;

namespace CQRS.WebChat.WebView
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}