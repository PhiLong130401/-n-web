using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SHOPMOBILE_PL.Models.BUS
{
    public class GioHangBUS
    {
        public static void Them(string masanpham, string mataikhoan, int soluong, int gia, string tensanpham, string HinhChinh)
        {

            using (var db = new ShopOnlineConnectionDB())
            {
                var x = db.Query<GioHang>("select * from GioHang Where MaTaiKhoan = '" + mataikhoan + "' and MaSanPham = '" + masanpham + "'").ToList();
                if (x.Count() > 0)
                {
                    // gọi hàm update so lượng
                    int a = (int)x.ElementAt(0).SoLuong + soluong;
                    CapNhat(masanpham, mataikhoan, a, gia, tensanpham, HinhChinh);
                }
                else
                {
                    GioHang giohang = new GioHang()
                    {
                        MaSanPham = masanpham,
                        MaTaiKhoan = mataikhoan,
                        SoLuong = soluong,
                        Gia = gia,
                        TenSanPham = tensanpham,
                        TongTien = gia * soluong,
                        HinhChinh = HinhChinh
                    };
                    db.Insert(giohang);
                }

            }
        }

        public static IEnumerable<GioHang> DanhSach(string mataikhoan)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                return db.Query<GioHang>("select * from GioHang where MaTaiKhoan = '" + mataikhoan + "'");
            }
        }
        public static void CapNhat(string masanpham, string mataikhoan, int soluong, int gia, string tensanpham, string HinhChinh)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                GioHang giohang = new GioHang()
                {
                    MaSanPham = masanpham,
                    MaTaiKhoan = mataikhoan,
                    SoLuong = soluong,
                    Gia = gia,
                    TenSanPham = tensanpham,
                    TongTien = gia * soluong,
                    HinhChinh = HinhChinh
                };
                var tamp = db.Query<GioHang>("Select idGH from GioHang Where MaTaiKhoan = '" + mataikhoan + "' and MaSanPham = '" + masanpham + "'").FirstOrDefault();
                db.Update(giohang, tamp.idGH);
            }
        }
        public static void Xoa(string masanpham, string mataikhoan)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                var a = db.Query<GioHang>("select * from GioHang where MaSanPham = '" + masanpham + "' and MaTaiKhoan ='" + mataikhoan + "'").FirstOrDefault();
                db.Delete(a);
            }
        }
        public static int TongTien(string mataikhoan)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                List<GioHang> a = DanhSach(mataikhoan).ToList();
                if (a.Count() == 0)
                {
                    return 0;
                }
                return db.Query<int>("select sum(TongTien) from GioHang where MaTaiKhoan = '" + mataikhoan + "' ").FirstOrDefault();

            }
        }
        public static SanPham ChiTiet(String a)
        {
            var db = new ShopOnlineConnectionDB();
            return db.SingleOrDefault<SanPham>("select * from SanPham where MaSanPham = @0", a);
        }
        /*//----------------------------------update images---------------
        public static void UpdateImages(string id, string images)
        {
            var db = new ShopOnlineConnectionDB();
            var sp = ShopOnline.ChiTiet(id);
            sp.HinhChinh = images;
            db.Update(sp, id);
        }*/
        //------------------------Loai ảnh đại diện cho hình ảnh-------------
        /*public static string LoadAvartaImg(string id)
        {
            var sp = ChiTiet(id);

            var product = ShopOnline.ChiTiet(id);
            var images = product.HinhChinh;
            XElement xImages = XElement.Parse(images);
            List<string> listImageReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImageReturn.Add(element.Value);
            }
            if (listImageReturn.Count() == 0)
            {
                return "/Asset/data/images/default.png";
            }
            return listImageReturn.ElementAt(0).ToString();
        }*/
    }
}