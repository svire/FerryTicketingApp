using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projektnippp.Models;

namespace Projektnippp.Controllers
{
    public class RacuniController : Controller
    {
        private BrodoviEntitity db = new BrodoviEntitity();

        // GET: Racuni
        public ActionResult Index(string racunid,string status)
        {
            int racun;


            if (int.TryParse(racunid, out racun))
                {

                var query = db.Racuns.Where(r => r.RacunId.Equals(racun)).ToList();
                return View(query);
                }



            if (status != null)
            {

                var query = db.Racuns.Where(r => r.Status.Contains(status)).ToList();
                return View(query);
            }
            else
                return View(db.Racuns.ToList());
        }

        // GET: Racuni/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // GET: Racuni/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.KartaId = new SelectList(db.Kartas, "KartaId", "Status");
            return View();
        }

        // POST: Racuni/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacunId,KartaId,Iznos,Status,DatumVrijeme")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Racuns.Add(racun);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    ViewBag.Greska = "Greska pri kreiranju racuna";
                }
            }

            ViewBag.KartaId = new SelectList(db.Kartas, "KartaId", "Status", racun.KartaId);
            return View(racun);
        }

        // GET: Racuni/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KartaId = new SelectList(db.Kartas, "KartaId", "Status", racun.KartaId);
            return View(racun);
        }

        // POST: Racuni/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RacunId,KartaId,Iznos,Status,DatumVrijeme")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(racun).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    ViewBag.Greska = "Greska pri upisu promjena";
                }
            }
            ViewBag.KartaId = new SelectList(db.Kartas, "KartaId", "Status", racun.KartaId);
            return View(racun);
        }

        // GET: Racuni/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racuni/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuns.Find(id);
            try
            {
                db.Racuns.Remove(racun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                ViewBag.Greska = "Doslo je do greske pri brisanju";
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
