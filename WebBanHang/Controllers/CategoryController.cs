using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WebBanHangEntities2 objwebBanHangEntities = new WebBanHangEntities2();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objwebBanHangEntities.Categories.ToList();

            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objwebBanHangEntities.Product.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}