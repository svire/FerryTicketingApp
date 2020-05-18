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
    public class VoznjeController : Controller
    {
        private BrodoviEntitity db = new BrodoviEntitity();

        // GET: Voznje
        public ActionResult Index(string polaziste, string dolaziste, string datum)
        {
            if (polaziste != null || dolaziste != null || datum != null)
            {

                var query = db.Voznjas.Where(x => x.Polaziste.Contains(polaziste) && x.Dolaziste.Contains(dolaziste) && x.Datum_polaska.Contains(datum)).ToList();
                //  var quer = db.Brods.Where(x => x.Tip.Contains(tip) && x.Naziv.Contains(naziv) && x.Registracija.Contains(registracija)).ToList();
                return View(query);
            }

            var voznjas = db.Voznjas.Include(v => v.Brod);
            return View(voznjas.ToList());
        }

        // GET: Voznje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voznja voznja = db.Voznjas.Find(id);
            if (voznja == null)
            {
                return HttpNotFound();
            }
            return View(voznja);
        }

        // GET: Voznje/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.BrodId = new SelectList(db.Brods, "BrodId", "Naziv");
            return View();
        }

        // POST: Voznje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoznjaId,Skracenica,Polaziste,Dolaziste,Vrijeme_polaska,Datum_polaska,BrodId,Slobodna_mjesta,Cijena")] Voznja voznja)
        {
            if (ModelState.IsValid)
            {
                try{ 

                var query = db.Voznjas.FirstOrDefault(v => v.Skracenica == voznja.Skracenica && v.Datum_polaska == voznja.Datum_polaska);
                if (query == null)
                {

                    db.Voznjas.Add(voznja);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                else
                {
                    //iako je mogucee da 2 broda krenu u isto vrijeme sa iste stanice U REALNOSTI
                    // NIJE PRAKSA PA STAVLJAMO OVO OBAVJESTENJE
                    ViewBag.BrodId = new SelectList(db.Brods, "BrodId", "Naziv", voznja.BrodId);
                    ModelState.AddModelError("Skracenica", "Ta voznja vec postoji DODAJ(n...) i novi datum");
                    ModelState.AddModelError("Datum_polaska", "Dodaj novi datum");
                    return View(voznja);

                }
               }
                catch(Exception)
                {
                    ViewBag.Greska = "Doslo je do greske prilikom unosa nove voznje";
                }

            }

            ViewBag.BrodId = new SelectList(db.Brods, "BrodId", "Naziv", voznja.BrodId);
            return View(voznja);
        }

        // GET: Voznje/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voznja voznja = db.Voznjas.Find(id);
            if (voznja == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrodId = new SelectList(db.Brods, "BrodId", "Naziv", voznja.BrodId);
            return View(voznja);
        }

        // POST: Voznje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoznjaId,Skracenica,Polaziste,Dolaziste,Vrijeme_polaska,Datum_polaska,BrodId,Slobodna_mjesta,Cijena")] Voznja voznja)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(voznja).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    ViewBag.Greska = "Greska prilikom promjene podataka o voznji";
                }
            }
            ViewBag.BrodId = new SelectList(db.Brods, "BrodId", "Naziv", voznja.BrodId);
            return View(voznja);
        }

        // GET: Voznje/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voznja voznja = db.Voznjas.Find(id);
            if (voznja == null)
            {
                return HttpNotFound();
            }
            return View(voznja);
        }

        // POST: Voznje/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voznja voznja = db.Voznjas.Find(id);
            try
            {
                db.Voznjas.Remove(voznja);
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
