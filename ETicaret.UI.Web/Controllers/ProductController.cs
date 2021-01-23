using ETicaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Web.Controllers
{
    public class ProductController : ETicControllerBase
    {
        DbETicaret db = new DbETicaret();
        // GET: Product
        [Route("Urun/{title}/{id}")]
        public ActionResult Detail(string title, int id)
        {
            ViewBag.IsLogin = this.IsLogin;
            var product = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return View(product);
        }
    }
}