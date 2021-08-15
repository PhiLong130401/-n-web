using Microsoft.AspNet.Identity;
using SHOPMOBILE_PL.Models.BUS;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOPMOBILE_PL.Controllers
{
    [Authorize]
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        public ActionResult Index()
        {
            List<GioHang> ds = GioHangBUS.DanhSach(User.Identity.GetUserId()).ToList();
            if (ds.Count() == 0)
            {
                return RedirectToAction("../Shop/index");
            }
            ViewBag.TongTien = GioHangBUS.TongTien(User.Identity.GetUserId());
            return View(ds);
        }
        [HttpPost]
        public ActionResult Them(string nguoinhan, string sdt, string diachi)
        {
            try
            {
                ThanhToan.ThemOrder(nguoinhan, sdt, diachi, User.Identity.GetUserId());
                return RedirectToAction("../Shop/Index");
            }
            catch
            {
                return RedirectToAction("../GioHang/Index");
            }

        }
    }
}