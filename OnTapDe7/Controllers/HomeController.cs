using OnTapDe7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnTapDe7.Controllers
{
    public class HomeController : Controller
    {
        QLBanHangQuanAoEntities db = new QLBanHangQuanAoEntities();

        public ActionResult Index()
        {
            var pl = db.PhanLoais.ToList();
            var sp = db.SanPhams.ToList();
            ViewBag.pl = pl;
            return View(sp);
        }

        [HttpPost]
        public ActionResult GetProductByCategory(string phanloai)
        {
            if (phanloai == "Tất cả sản phẩm")
            {
                // lấy ra tất cả sản phẩm
                var sanPham1 = db.SanPhams.ToList();

                // bỏ các thuộc tính khóa ngoại
                var _sanPham1 = sanPham1
                .Select(sp => new SanPham
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    DonGiaBanNhoNhat = sp.DonGiaBanNhoNhat,
                    DonGiaBanLonNhat = sp.DonGiaBanLonNhat,
                    TrangThai = sp.TrangThai,
                    MoTaNgan = sp.MoTaNgan,
                    AnhDaiDien = sp.AnhDaiDien,
                    NoiBat = sp.NoiBat,
                    MaPhanLoaiPhu = sp.MaPhanLoaiPhu,
                    MaPhanLoai = sp.MaPhanLoai,
                    GiaNhap = sp.GiaNhap
                }).ToList();

                return Json(new { success = true, sanPham = _sanPham1 });
            }
            var sanPham = db.SanPhams
                .Where(sp => sp.PhanLoai.PhanLoaiChinh == phanloai)
                .ToList();

            // bỏ các thuộc tính khóa ngoại
            var _sanPham = sanPham
                .Select(sp => new SanPham
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    DonGiaBanNhoNhat = sp.DonGiaBanNhoNhat,
                    DonGiaBanLonNhat = sp.DonGiaBanLonNhat,
                    TrangThai = sp.TrangThai,
                    MoTaNgan = sp.MoTaNgan,
                    AnhDaiDien = sp.AnhDaiDien,
                    NoiBat = sp.NoiBat,
                    MaPhanLoaiPhu = sp.MaPhanLoaiPhu,
                    MaPhanLoai = sp.MaPhanLoai,
                    GiaNhap = sp.GiaNhap
                }).ToList();

            return Json(new { success = true, sanPham = _sanPham });
        }

        public ActionResult AddNewProduct()
        {
            // Lấy dữ liệu từ cơ sở dữ liệu hoặc các nguồn khác
            var phanLoaiChinh = db.PhanLoais.ToList();
            
            var phanLoaiPhu = db.PhanLoaiPhus.ToList();

            // Tạo SelectList
            ViewBag.PhanLoaiChinh = new SelectList(phanLoaiChinh, "MaPhanLoai", "PhanLoaiChinh");
            ViewBag.PhanLoaiPhu = new SelectList(phanLoaiPhu, "MaPhanLoaiPhu", "TenPhanLoaiPhu");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}