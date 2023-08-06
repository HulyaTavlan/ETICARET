using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppEF.Data;
using WebAppEF.Entities;

namespace WebAppEF.Controllers
{
    public class ProductsController : Controller
    {
        private ProductRepository _repo = new ProductRepository();
        private BrandRepository b_repo = new BrandRepository();
        private CategoryRepository c_repo = new CategoryRepository();

        // GET: Products
        public ActionResult Index()
        {
            //var products = db.Products.Include(p => p.Brand).Include(p => p.Category);

            var model = new Tuple<IEnumerable<Product>, IEnumerable<Category>, IEnumerable<Brand>>(_repo.ReadAll("Brand,Category"), c_repo.ReadAll(), b_repo.ReadAll());

            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _repo.ReadOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(
                b_repo.ReadMany(x => x.Active && !x.Deleted),
                "Id", "Title");
            ViewBag.CategoryId = new SelectList(
                c_repo.ReadMany(x => x.Active && !x.Deleted),
                "Id", "Title");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Discounted,DiscountRatio,TaxRatio,Detail,FeaturedImage,CategoryId,BrandId,Title,CreateDate,Active,Deleted")] Product product, HttpPostedFileBase FeaturedImage)
        {
            if (FeaturedImage != null && FeaturedImage.ContentLength > 0)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(FeaturedImage.FileName);
                product.FeaturedImage = filename;
                string path = Path.Combine(Server.MapPath("~/images"), filename);
                FeaturedImage.SaveAs(path);
            }
            else
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _repo.Create(product);
                _repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(
                b_repo.ReadMany(x => x.Active && !x.Deleted),
                "Id", "Title", product.BrandId);
            ViewBag.CategoryId = new SelectList(
                c_repo.ReadMany(x => x.Active && !x.Deleted),
                "Id", "Title", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _repo.ReadOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(
               b_repo.ReadMany(x => x.Active && !x.Deleted),
               "Id", "Title", product.BrandId);
            ViewBag.CategoryId = new SelectList(
                c_repo.ReadMany(x => x.Active && !x.Deleted),
                "Id", "Title", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Discounted,DiscountRatio,TaxRatio,Detail,FeaturedImage,CategoryId,BrandId,Title,CreateDate,Active,Deleted")] Product product, HttpPostedFileBase FeaturedImage, string oldFeaturedImage)
        {
            if (FeaturedImage != null && FeaturedImage.ContentLength > 0)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(FeaturedImage.FileName);
                product.FeaturedImage = filename;
                string path = Path.Combine(Server.MapPath("~/images"), filename);
                FeaturedImage.SaveAs(path);
            }
            else
            {
                product.FeaturedImage = oldFeaturedImage;
            }

            if (ModelState.IsValid)
            {
                _repo.Update(product);
                _repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(
               b_repo.ReadMany(x => x.Active && !x.Deleted),
               "Id", "Title", product.BrandId);
            ViewBag.CategoryId = new SelectList(
                c_repo.ReadMany(x => x.Active && !x.Deleted),
                "Id", "Title", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _repo.ReadOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _repo.ReadOne(id);
            _repo.Delete(product);
            _repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Toggle(string type, int id)
        {
            Product p = _repo.ReadOne(id);
            if (p != null)
            {
                switch (type)
                {
                    case "active": p.Active = !p.Active; break;
                    case "deleted": p.Deleted = !p.Deleted; break;
                    case "discounted": p.Discounted = !p.Discounted; break;
                    default: break;
                }
                _repo.Update(p);
                _repo.Save();
            }
            return RedirectToAction("Index");
        }
    }
}
