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
    public class KarteController : Controller
    {
        private BrodoviEntitity db = new BrodoviEntitity();

        // GET: Karte
        public ActionResult Index(string kartaid)
        {
            if(kartaid!=null)
            {
                int karta = int.Parse(kartaid);
                
                var query = db.Kartas.Where(k => k.KartaId.Equals(karta)).ToList();
                return View(query);
            }


            var kartas = db.Kartas.Include(k => k.Putnik1).Include(k => k.Voznja1);
            return View(kartas.ToList());
        }

        // GET: Karte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karta karta = db.Kartas.Find(id);
            if (karta == null)
            {
                return HttpNotFound();
            }
            return View(karta);
        }

        // GET: Karte/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Putnik = new SelectList(db.Putniks, "PutnikId", "ImePrezime");
            ViewBag.Voznja = new SelectList(db.Voznjas, "VoznjaId", "Skracenica");
            return View();
        }

        // POST: Karte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KartaId,Putnik,Voznja,Sjediste_Broj,Status")] Karta karta)
        {
            if (ModelState.IsValid)
            {
                var query = db.Kartas.FirstOrDefault(k => k.Voznja == karta.Voznja && k.Sjediste_Broj == karta.Sjediste_Broj);
                var data = from voz in db.Voznjas where voz.VoznjaId == karta.Voznja select voz.Slobodna_mjesta;
                var slobodna = data.ToList()[0].ToString();
                int slobpars = int.Parse(slobodna);
                if (query == null&&slobpars>0)
                {
                    db.Kartas.Add(karta);
                    db.SaveChanges();

                    //SMANJI BROJ SLOBODNIH MJESTA ZA VOZNJU(ZA KOJU JE KARTA IZDATA)
                    slobpars--;
                    var data1 = from voz in db.Voznjas where voz.VoznjaId == karta.Voznja select voz;
                    Voznja cv = data1.ToList()[0];
                    cv.Slobodna_mjesta = slobpars;
                    db.SaveChanges();

                    
                    //UZIMAMO VRIJEDNOST CIJENE KARTE ZA IZABRANU VOZNJU KAKO BISMO NAPRAVILI RACUN
                    var data3 = from cij in db.Voznjas where cij.VoznjaId == karta.Voznja select cij.Cijena;
                    var cijena = data3.ToList()[0].ToString();
                    

                    //DODAVANJE NOVOG RACUNA ZA KARTU RADI NE DIRAJ
                    Racun racun = new Racun();
                   // racun.RacunId = 1; AUTOINCREMENT
                    racun.KartaId = karta.KartaId;
                    racun.Iznos = Convert.ToDecimal(cijena);
                    racun.Status = "Narucena";
                    racun.DatumVrijeme = DateTime.Now.ToString();
                    db.Racuns.Add(racun);
                    db.SaveChanges();



                    return RedirectToAction("Index");
                }

                else
                {
                    ViewBag.Putnik = new SelectList(db.Putniks, "PutnikId", "ImePrezime", karta.Putnik);
                    ViewBag.Voznja = new SelectList(db.Voznjas, "VoznjaId", "Skracenica", karta.Voznja);
                    ModelState.AddModelError("Sjediste_Broj", "Sjediste za tu voznju je zauzeto, unesite drugo");
                    ModelState.AddModelError("Status","NEMA SLOBODNIH SJEDISTA");
                    //  var data = from c in db.Voznjas where c.VoznjaId == karta.Voznja select c.Slobodna_mjesta;
                    //  var dae = db.Voznjas.Where(v => v.VoznjaId = karta.Voznja);
                    //  var parsiran = data.ToList()[0].ToString();
                    //  int pars = int.Parse(parsiran);pars--;
                    //   ModelState.AddModelError("Status", slobpars.ToString());


                    return View(karta);
                }
            }

            ViewBag.Putnik = new SelectList(db.Putniks, "PutnikId", "ImePrezime", karta.Putnik);
            ViewBag.Voznja = new SelectList(db.Voznjas, "VoznjaId", "Skracenica", karta.Voznja);
            return View(karta);
        }

        // GET: Karte/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karta karta = db.Kartas.Find(id);
            if (karta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Putnik = new SelectList(db.Putniks, "PutnikId", "ImePrezime", karta.Putnik);
            ViewBag.Voznja = new SelectList(db.Voznjas, "VoznjaId", "Skracenica", karta.Voznja);
            return View(karta);
        }

        // POST: Karte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KartaId,Putnik,Voznja,Sjediste_Broj,Status")] Karta karta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(karta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Putnik = new SelectList(db.Putniks, "PutnikId", "ImePrezime", karta.Putnik);
            ViewBag.Voznja = new SelectList(db.Voznjas, "VoznjaId", "Skracenica", karta.Voznja);
            return View(karta);
        }

        // GET: Karte/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karta karta = db.Kartas.Find(id);
            if (karta == null)
            {
                return HttpNotFound();
            }
            return View(karta);
        }

        // POST: Karte/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            //prije nego obrisemo kartu brisemo racun izdat za istu(sta ce nam)
            //baza je struktuirana tako da svakko drugacije se i ne bi mogla izbrisati karta
            //jer je generisani racun ukazuje na KartaId.
            var racunbrisi = from racuni in db.Racuns where racuni.KartaId == id select racuni.RacunId;
            var racunbroj = racunbrisi.ToList()[0].ToString();
            int racunint = int.Parse(racunbroj);
            Racun racuncic = db.Racuns.Find(racunint);
            db.Racuns.Remove(racuncic);


            //increment slobodnih mjesta(nakon brisanja karte za neku voznju)
            //oslobodilo se mjesto koje sad moze neko drugi da uzme
            
            //uzima vrijednost VoznjaId iz Karte koju brisemo
            var vrijednostVoznje = from vozkar in db.Kartas where vozkar.KartaId == id select vozkar.Voznja;
            var vrijVoz = vrijednostVoznje.ToList()[0].ToString();
            int vrijVozint = int.Parse(vrijVoz);

            //inkrement vrijednosti nakon brisanja OSLOBODJENO MJESTO
            var data = from voz in db.Voznjas where voz.VoznjaId == vrijVozint select voz.Slobodna_mjesta;
            var slobodna = data.ToList()[0].ToString();
            int slobpars = int.Parse(slobodna);
            slobpars++;
            var data1 = from voz in db.Voznjas where voz.VoznjaId == vrijVozint select voz;
            Voznja cv = data1.ToList()[0];
            cv.Slobodna_mjesta = slobpars;
            db.SaveChanges();




            Karta karta = db.Kartas.Find(id);
            db.Kartas.Remove(karta);
            db.SaveChanges();
            return RedirectToAction("Index");
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
