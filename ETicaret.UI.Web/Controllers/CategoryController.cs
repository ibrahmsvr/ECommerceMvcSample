using ETicaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Web.Controllers
{
    public class CategoryController : ETicControllerBase
    {
        DbETicaret db = new DbETicaret();
        // GET: Category
        //2. Id sayfalandırma için
        [Route("Kategori/{isim}/{id}")]
        public ActionResult Index(string isim, int id)
        {
            ViewBag.isLogin = this.IsLogin;
            //ViewBag.sayi = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(db.Products.Where(x => x.CategoryID == id).Count()) / 8)); ;
            var data = db.Products.Where(x => x.IsActive == true && x.CategoryID == id).ToList();
            ViewBag.category = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
    }
}