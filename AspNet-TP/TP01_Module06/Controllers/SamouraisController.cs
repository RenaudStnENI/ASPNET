using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO_Samourai;
using TP01_Module06.Data;
using TP01_Module06.Models;

namespace TP01_Module06.Controllers
{
    public class SamouraisController : Controller
    {
        private SamouraiContext db = new SamouraiContext();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SamouraisVM vm = new SamouraisVM();
            vm.Samourai = db.Samourais.Find(id);
            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }

            return View(vm);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraisVM vm = new SamouraisVM();
            this.getArmes(vm);

            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraisVM vm)
        {
            if (ModelState.IsValid)
            {
                Samourai sam = vm.Samourai;
                sam.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdArme);
                db.Samourais.Add(vm.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(this.getArmes(vm));
            }
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SamouraisVM vm = new SamouraisVM();
            vm.Samourai = db.Samourais.Find(id);
            this.getArmes(vm);
            if (vm.Samourai.Arme != null)
            {
                vm.IdArme = vm.Samourai.Arme.Id;
            }
            else
            {
                vm.IdArme = null;

            }

            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraisVM vm)
        {
            if (ModelState.IsValid)
            {
                Samourai modifiedSam = db.Samourais.Include(s => s.Arme).FirstOrDefault(x => x.Id == vm.Samourai.Id);

                modifiedSam.Arme = vm.IdArme == null ? null : db.Armes.FirstOrDefault(x => x.Id == vm.IdArme);

                db.Entry(modifiedSam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SamouraisVM vm = new SamouraisVM();
            vm.Samourai = db.Samourais.Find(id);
            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }

            return View(vm);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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

        private SamouraisVM getArmes(SamouraisVM vm)
        {
            vm.Armes = db.Armes.Select(p => new SelectListItem()
                    {Text = p.Nom, Value = p.Id.ToString()})
                .ToList();

            return vm;
        }
    }
}