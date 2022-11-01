using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            var degerler = db.tbl_Urunler.ToList();

            return View(degerler);
        }

        [HttpGet]

        public ActionResult YeniUrun()

        {
            List<SelectListItem> degerler = (from i in db.tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]

        public ActionResult YeniUrun(tbl_Urunler p1)
        {
            var ktg = db.tbl_Kategoriler.Where(m => m.KategoriID == p1.tbl_Kategoriler.KategoriID).FirstOrDefault();
            p1.tbl_Kategoriler = ktg;

            db.tbl_Urunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.tbl_Urunler.Find(id);
            db.tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.tbl_Urunler.Find(id);

            List<SelectListItem> degerler = (from i in db.tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);

        }

        public ActionResult Guncelle(tbl_Urunler p)
        {
            var urun = db.tbl_Urunler.Find(p.UrunID);
            urun.UrunAd = p.UrunAd;
            //urun.UrunKategori = p.UrunKategori;
            urun.Fiyat = p.Fiyat;
            urun.Stok = p.Stok;
            var ktg = db.tbl_Kategoriler.Where(m => m.KategoriID == p.tbl_Kategoriler.KategoriID).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}