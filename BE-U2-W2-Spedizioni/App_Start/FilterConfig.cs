﻿using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_Spedizioni
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
