using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        WebBanHangEntities2 objwebBanHangEntities = new WebBanHangEntities2();

        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objwebBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}