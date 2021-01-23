using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ETicaret.UI.Web.Areas.Admin
{
    public class AdminControllerBase: Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var IsLogin = false;
            if (requestContext.HttpContext.Session["AdminLoginUser"]==null)
            {
                //Admin girişi yok
                requestContext.HttpContext.Response.Redirect("/Admin/AdminLogin");              
            }
            else
            {
                //Admin giriş yaptı, sayfayı çalış
            }
            base.Initialize(requestContext);    
        }
    }
}