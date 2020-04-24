using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

            this.getArmesAndArtMartials(vm);

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
                if (vm.IdArme != null)
                {
                    Samourai samAvecArme = db.Samourais.FirstOrDefault(s => s.Arme.Id == vm.IdArme);
                    if (samAvecArme != null)
                    {
                        samAvecArme.Arme = null;
                        db.Entry(samAvecArme).State = EntityState.Modified;
                    }

                    vm.Samourai.Arme = db.Armes.Find(vm.IdArme);
                }

                vm.Samourai.ArtMartials = db.ArtMartials.Where(am => vm.IdsArtMartial.Contains(am.Id)).ToList();

                db.Samourais.Add(vm.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.getArmesAndArtMartials(vm);

            return View(vm);
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
            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }

            //
            List<int> listeArmesId =
                db.Samourais.Where(x => x.Arme != null && x.Id != id).Select(x => x.Arme.Id).ToList();
            vm.ArmeItems = db.Armes.Where(a => !listeArmesId.Contains(a.Id)).Select(a => new SelectListItem()
                    {Text = a.Nom, Value = a.Id.ToString()})
                .ToList();
            //
            vm.ArtMartialItems = db.ArtMartials.Select(am => new SelectListItem()
                    {Text = am.Nom, Value = am.Id.ToString()})
                .ToList();
            //
            vm.IdsArtMartial = vm.Samourai.ArtMartials.Select(s => s.Id).ToList();
            //
            if (vm.Samourai.Arme != null)
            {
                vm.IdArme = vm.Samourai.Arme.Id;
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
                //
                Samourai modifiedSam = db.Samourais.Include(x => x.Arme).FirstOrDefault(x => x.Id == vm.Samourai.Id);
                //
                modifiedSam.Nom = vm.Samourai.Nom;
                //
                modifiedSam.Force = vm.Samourai.Force;
                //
                foreach (var am in modifiedSam.ArtMartials)
                {
                    db.Entry(am).State = EntityState.Modified;
                }

                modifiedSam.ArtMartials = db.ArtMartials.Where(am => vm.IdsArtMartial.Contains(am.Id)).ToList();
                //
                if (vm.IdArme != null)
                {
                    modifiedSam.Arme = db.Armes.FirstOrDefault(a => a.Id == vm.IdArme);
                }
                else
                {
                    var samouraisAvecMonArme = db.Samourais.FirstOrDefault(x => x.Arme.Id == vm.IdArme);
                    samouraisAvecMonArme.Arme = null;
                    db.Entry(samouraisAvecMonArme).State = EntityState.Modified;

                    modifiedSam.Arme = null ?? db.Armes.FirstOrDefault(x => x.Id == vm.IdArme);
                }


                db.Entry(modifiedSam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //
            List<int> listeArmesId =
                db.Samourais.Where(x => x.Arme != null && x.Id != vm.Samourai.Id).Select(x => x.Arme.Id).ToList();
            vm.ArmeItems = db.Armes.Where(a => !listeArmesId.Contains(a.Id)).Select(a => new SelectListItem()
                    {Text = a.Nom, Value = a.Id.ToString()})
                .ToList();
            //
            vm.ArtMartialItems = db.ArtMartials.Select(am => new SelectListItem()
                    {Text = am.Nom, Value = am.Id.ToString()})
                .ToList();
            //
            if (vm.Samourai.Arme != null)
            {
                vm.IdArme = vm.Samourai.Arme.Id;
            }

            //
            vm.IdsArtMartial = vm.Samourai.ArtMartials.Select(s => s.Id).ToList();

            return View(vm);
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
            foreach (var am in samourai.ArtMartials)
            {
                db.Entry(am).State = EntityState.Modified;
            }

            samourai.ArtMartials.Clear();

            db.Samourais.Remove(samourai);
            db.Entry(samourai).State = EntityState.Deleted;
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

        private SamouraisVM getArmesAndArtMartials(SamouraisVM vm)
        {
            List<int> listeArmesId = db.Samourais.Where(s => s.Arme != null).Select(s => s.Arme.Id).ToList();

            vm.ArmeItems = db.Armes.Where(a => !listeArmesId.Contains(a.Id)).Select(a => new SelectListItem()
                    {Text = a.Nom, Value = a.Id.ToString()})
                .ToList();

            vm.ArtMartialItems = db.ArtMartials.Select(am => new SelectListItem()
                    {Text = am.Nom, Value = am.Id.ToString()})
                .ToList();

            return vm;
        }
    }
}