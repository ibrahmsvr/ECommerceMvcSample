using ETicaret.Core.Model;
using ETicaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Web.Controllers
{
    public class HomeController : ETicControllerBase
    {
        DbETicaret db = new DbETicaret();
        // GET: Home
        //Id 1 , ana sayfada ilk açıldığında direk 1. sayfayı gösterir.
        public ActionResult Index(int Id=1)
        {
            ViewBag.IsLogin = this.IsLogin;
            ViewBag.sayi = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(db.Products.Count()) / 8)); ;
            
            var data = db.Products.Include("Category").Where(x=>x.IsActive==true).OrderByDescending(x => x.CreateDate).Skip((Id - 1) * 8).Take(8).ToList();
            return View(data);
        }

        public PartialViewResult GetMenu()
        {
           
            var menus = db.Categories.Where(x => x.ParentID == 0).ToList();
            return PartialView(menus);
        }
        
        [Route("Uye-Giris")]
        public ActionResult Login()
        {           
            return View();
        }

        [HttpPost]
        [Route("Uye-Giris")]
        public ActionResult Login(string Email, string Password)
        {
            var users = db.Users.Where(x => x.Email == Email
            && x.Password == Password
            && x.IsActive == true
            && x.IsAdmin == false).ToList();

            if (users.Count==1)
            {
                //giriş başarılı
                Session["LoginUserID"] = users.FirstOrDefault().ID;
                Session["LoginUser"] = users.FirstOrDefault();
                //anasayfaya git
                return Redirect("/");
            }
            else
            {
                ViewBag.Error = "Hatalı Kullanıcı Adı veya Şifre..";
                return View();
            }
            
        }
        [Route("Uye-Kayit")]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Kayit")]
        public ActionResult CreateUser(User entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateUserID = 1;
                entity.IsActive = true;
                entity.IsAdmin = false;

                db.Users.Add(entity);
                db.SaveChanges();

                return Redirect("/");
            }
            catch (Exception)
            {

                return View();
            }
            
        }
    }
}