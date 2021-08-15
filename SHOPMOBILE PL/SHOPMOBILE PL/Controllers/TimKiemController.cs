using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPMOBILE_PL.Models;
using PagedList.Mvc;
using PagedList;
using SHOPMOBILE_PL.Models.BUS;

namespace SHOPMOBILE_PL.Controllers
{

    public class TimKiemController : Controller
    {
        // GET: TimKiem
        public ActionResult KetQuaTimKiem(string timkiem, string MaNhaSanXuat, string MaLoaiSanPham, string GiaBatDau, string GiaKetThuc, int page = 1, int pagesize = 3)
        {
            ViewBag.MaNhaSanXuat = new SelectList(NhaSanXuatBUS.List(), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaLoaiSanPham = new SelectList(LoaiSanPhamBUS.List(), "MaLoaiSanPham", "TenLoaiSanPham");
            var db = TimKiemBUS.TimKiem(timkiem, MaNhaSanXuat, MaLoaiSanPham, GiaBatDau, GiaKetThuc).ToPagedList(page, pagesize);
            return View(db);
        }

    }
}