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
    public class PutniciController : Controller
    {
        private BrodoviEntitity db = new BrodoviEntitity();

        // GET: Putnici
        public ActionResult Index(string imeprezime, string adresa, string email)
        {
            if (imeprezime != null || adresa != null || email != null)
            {
                var query = db.Putniks.Where(x => x.ImePrezime.Contains(imeprezime) && x.Adresa.Contains(adresa) && x.Email.Contains(email)).ToList();
                return View(query);
            }
            return View(db.Putniks.ToList());
        }

        // GET: Putnici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Putnik putnik = db.Putniks.Find(id);
            if (putnik == null)
            {
                return HttpNotFound();
            }
            return View(putnik);
        }

        // GET: Putnici/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Putnici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PutnikId,ImePrezime,Godine,MjestoStanovanja,Adresa,Telefon,Email")] Putnik putnik)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = db.Putniks.FirstOrDefault(p => p.Email == putnik.Email);
                    if (query == null)
                    {

                        db.Putniks.Add(putnik);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Taj email vec neko koristi");
                        return View(putnik);
                    }
                }
                catch (Exception)
                {
                    ViewBag.Greska = "Doslo je do greske prilikom upisa putnika";
                }
            }

            return View(putnik);
        }

        // GET: Putnici/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Putnik putnik = db.Putniks.Find(id);
            if (putnik == null)
            {
                return HttpNotFound();
            }
            return View(putnik);
        }

        // POST: Putnici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PutnikId,ImePrezime,Godine,MjestoStanovanja,Adresa,Telefon,Email")] Putnik putnik)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(putnik).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    ViewBag.Greska = "Greska pri upisu promjena";
                }
            }
            return View(putnik);
        }

        // GET: Putnici/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Putnik putnik = db.Putniks.Find(id);
            if (putnik == null)
            {
                return HttpNotFound();
            }
            return View(putnik);
        }

        // POST: Putnici/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Putnik putnik = db.Putniks.Find(id);
            try
            {
                db.Putniks.Remove(putnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                ViewBag.Greska = "Doslo je greske pri brisanju";
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
