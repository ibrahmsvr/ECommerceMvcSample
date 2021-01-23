using ETicaret.Core.Model;
using ETicaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Web.Controllers
{
    public class OrderController : ETicControllerBase
    {
        DbETicaret db = new DbETicaret();

        [Route("SiparisVer")]
        // GET: Order
        public ActionResult AddressList()
        {
            var data = db.UserAddresses.Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult CreateUserAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUserAddress(UserAddress entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.CreateUserID = LoginUserID;
            entity.IsActive = true;
            entity.UserID = LoginUserID;

            db.UserAddresses.Add(entity);
            db.SaveChanges();
            return RedirectToAction("AddressList");
        }

        public ActionResult CreateOrder(int id)
        {
            var sepet = db.Carts.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            Order o = new Order();
            o.CreateDate = DateTime.Now;
            o.CreateUserID = LoginUserID;
            //Ödeme Bekleniyor
            o.StatusID = 1;   
            o.TotalProductPrice = sepet.Sum(x => x.Product.Price);
            o.TotalTaxPrice = sepet.Sum(x => x.Product.Tax);
            o.TotalDiscount = sepet.Sum(x => x.Product.Discount);
            o.TotalPrice = o.TotalProductPrice + o.TotalTaxPrice;
            o.UserAddressID = id;
            o.UserID = LoginUserID;

            o.OrderProducts = new List<OrderProduct>();
            foreach (var item in sepet)
            {
                o.OrderProducts.Add(new OrderProduct
                {
                    CreateDate = DateTime.Now,
                    CreateUserID = LoginUserID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity
                });
                db.Carts.Remove(item);
            }

            db.Orders.Add(o);
            db.SaveChanges();
            //var orderid = db.Orders.Where(x => x.UserID == LoginUserID).LastOrDefault().ID;
            return RedirectToAction("Detail", new { id= o.ID });
        }

        public ActionResult Detail(int id)
        {
            var data = db.Orders.Include("OrderProducts")
                .Include("OrderProducts.Product")
                .Include("OrderPayments")
                .Include("Status")
                .Include("UserAddress").Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [Route("Siparislerim")]
        public ActionResult Index()
        {
            var data = db.Orders.Include("Status").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult Pay(int id)
        {
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            //Ödeme kontrolü bekliyor
            order.StatusID = 7;
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = order.ID });
        }
    }
}