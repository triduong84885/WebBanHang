 using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using static WebBanHang.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        WebBanHangEntities2 objwebBanHangEntities = new WebBanHangEntities2();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objwebBanHangEntities.Product.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objwebBanHangEntities.Product.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpLoad != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                    fileName = fileName + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                objwebBanHangEntities.Product.Add(objProduct);
                objwebBanHangEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objwebBanHangEntities.Product.Add(objProduct);
                    objwebBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }

        void LoadData()
        {
            Common objCommon = new Common();
            var lstCat = objwebBanHangEntities.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            var lstBrand = objwebBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Dề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objwebBanHangEntities.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objwebBanHangEntities.Product.Where(n => n.Id == id).FirstOrDefault();

            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objwebBanHangEntities.Product.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objwebBanHangEntities.Product.Remove(objProduct);
            objwebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objwebBanHangEntities.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(Product objPro)
        {
            if (objPro.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objPro.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objPro.ImageUpLoad.FileName);
                fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objPro.Avatar = fileName;
                objPro.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objwebBanHangEntities.Entry(objPro).State = EntityState.Modified;
            objwebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}