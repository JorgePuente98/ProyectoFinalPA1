using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaVideojuegos.DAL;
using TiendaVideojuegos.Models;

namespace TiendaVideojuegos.Controllers
{
    public class CarritoItemsController : Controller
    {
        private TiendaContext db = new TiendaContext();

        // GET: CarritoItems
        public ActionResult Index()
        {
            //var carritoItems = db.CarritoItems.Include(c => c.Producto);
            var carritoItems = CarritoProductos.listaProductos;
            return View(carritoItems);
        }

        // GET: CarritoItems/Details/5
        public ActionResult Details(CarritoItem unArticulo)
        {
            return View(unArticulo);
        }

        // GET: CarritoItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre");
            return View();
        }

        // POST: CarritoItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarritoID,ProductoID,Cantidad")] CarritoItem carritoItem)
        {
            if (ModelState.IsValid)
            {
                db.CarritoItems.Add(carritoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", carritoItem.ProductoID);
            return View(carritoItem);
        }

        // GET: CarritoItems/Edit/5
        public ActionResult Edit(CarritoItem unArticulo)
        {
            if (unArticulo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (CarritoItem item in CarritoProductos.listaProductos)
            {
                if (item.Equals(unArticulo.ProductoID))
                {
                    item.Producto = unArticulo.Producto;
                }
            }
            if (unArticulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", unArticulo.ProductoID);
            return View(unArticulo);
        }

        // POST: CarritoItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CarritoID,ProductoID,Cantidad")] CarritoItem carritoItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(carritoItem).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", carritoItem.ProductoID);
        //    return View(carritoItem);
        //}

        // GET: CarritoItems/Delete/5
        public ActionResult Delete(CarritoItem unArticulo)
        {
            foreach (CarritoItem item in CarritoProductos.listaProductos)
            {
                if(item.ProductoID == unArticulo.ProductoID)
                {
                    unArticulo = item;
                }
            }
            return View(unArticulo);
        }

        // POST: CarritoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(CarritoItem unArticulo)
        {
            CarritoProductos.listaProductos.Remove(unArticulo);
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
