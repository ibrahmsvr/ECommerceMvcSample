using ETicaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ETicaret.UI.Web
{
    public class ETicControllerBase: Controller
    {
        //Kullanıcı login kontrol
        public bool IsLogin { get; private set; }
        //giriş yapanın ID' si
        public int LoginUserID { get; private set; }
        //Login user  entity
        public User LoginUserEntity { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            //Session["LoginUserID"] = users.FirstOrDefault().ID;
            //Session["LoginUser"] = users.FirstOrDefault();
            if (requestContext.HttpContext.Session["LoginUserID"] != null)
            {
                // Kullanıcı giriş yaptıysa
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUserID"];
                LoginUserEntity = (User)requestContext.HttpContext.Session["LoginUser"];
            }
            base.Initialize(requestContext);    
        }
    }
}