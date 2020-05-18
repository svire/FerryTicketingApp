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
    public class BrodoviController : Controller
    {
        private BrodoviEntitity db = new BrodoviEntitity();

        // GET: Brodovi
        public ActionResult Index(string tip, string naziv, string registracija)
        {
            if (tip != null || naziv != null || registracija != null)
            {
                var query = db.Brods.Where(x => x.Tip.Contains(tip) && x.Naziv.Contains(naziv) && x.Registracija.Contains(registracija)).ToList();
                return View(query);
            }

            return View(db.Brods.ToList());
        }

        // GET: Brodovi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brod brod = db.Brods.Find(id);
            if (brod == null)
            {
                return HttpNotFound();
            }
            return View(brod);
        }

        // GET: Brodovi/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brodovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrodId,Naziv,Tip,Registracija,Brojsjedista,Status")] Brod brod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = db.Brods.FirstOrDefault(b => b.Registracija == brod.Registracija);
                    if (query == null)
                    {
                        db.Brods.Add(brod);
                        db.SaveChanges();
                        //ako je uredu vraca na index
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Registracija", "Ta registracija vec postoji");
                        return View(brod);
                    }
                }
                catch(Exception)
                {
                    ViewBag.Greska = "Doslo je do greske pri upisu broda u bazu";
                }

            }

            return View(brod);
        }

        // GET: Brodovi/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brod brod = db.Brods.Find(id);
            if (brod == null)
            {
                return HttpNotFound();
            }
            return View(brod);
        }

        // POST: Brodovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrodId,Naziv,Tip,Registracija,Brojsjedista,Status")] Brod brod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(brod).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.Greska = "Greska pri upisu promjena";
                }
            }
            return View(brod);
        }

        // GET: Brodovi/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brod brod = db.Brods.Find(id);
            if (brod == null)
            {
                return HttpNotFound();
            }
            return View(brod);
        }

        // POST: Brodovi/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brod brod = db.Brods.Find(id);
            try
            {
                db.Brods.Remove(brod);
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
