using ETicaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Web.Controllers
{
    public class CartController : ETicControllerBase
    {
        DbETicaret db = new DbETicaret();
        [HttpPost]
        // GET: Cart
        public JsonResult AddProduct(int productID, int quantity)
        {
            db.Carts.Add(new Core.Model.Entity.Cart
            {
                CreateDate = DateTime.Now,
                CreateUserID = LoginUserID,
                ProductID = productID,
                Quantity = quantity,
                UserID= LoginUserID
            });
            var rt = db.SaveChanges();
            return Json(rt, JsonRequestBehavior.AllowGet);
        }

        [Route("Sepetim")]
        public ActionResult Index()
        {
            var data = db.Carts.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            
            decimal urunlertop = data.Sum(x => x.Product.Price);
            decimal KDV = data.Sum(x => x.Product.Tax);
            ViewBag.ToplamFiyat = urunlertop + KDV;
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var deleteitem = db.Carts.Where(x => x.ID == id).FirstOrDefault();
            db.Carts.Remove(deleteitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}