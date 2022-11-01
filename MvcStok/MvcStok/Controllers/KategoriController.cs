using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.tbl_Kategoriler.ToList();

            var degerler = db.tbl_Kategoriler.ToList().ToPagedList(sayfa,4);

            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]

        public ActionResult YeniKategori(tbl_Kategoriler p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniKategori");
            }

            db.tbl_Kategoriler.Add(p1);
            db.SaveChanges();
            return View();

        }

        public ActionResult Sil(int id)
        {
            var kategori = db.tbl_Kategoriler.Find(id);
            db.tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.tbl_Kategoriler.Find(id);

            return View("KategoriGetir", ktg);
        }

        public ActionResult Guncelle(tbl_Kategoriler p1) 
        {
            var ktg = db.tbl_Kategoriler.Find(p1.KategoriID);
            ktg.KategoriAd = p1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");

        
        }
    }
}