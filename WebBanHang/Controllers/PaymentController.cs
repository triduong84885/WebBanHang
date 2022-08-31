using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class PaymentController : Controller
    {
        WebBanHangEntities2 objwebBanHangEntities = new WebBanHangEntities2();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");

            }
            else
            {
                //lay t.tin gio hang từ biến sesion
                var lstCart = (List<CartModel>)Session["cart"];
                // gán dl cho Order
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 2;
                objwebBanHangEntities.Orders.Add(objOrder);
                //luu t.tin dl vào bảng Order
                objwebBanHangEntities.SaveChanges();
                // lấy oder id vừa mới tạo ra để lưu vào bảng OrderDetail
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objwebBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                objwebBanHangEntities.SaveChanges();
            }
            return View();
        }
    }
}