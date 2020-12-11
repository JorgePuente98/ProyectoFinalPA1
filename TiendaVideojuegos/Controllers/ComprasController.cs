using PagedList;
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
    public class ComprasController : Controller
    {
        private TiendaContext db = new TiendaContext();

        // GET: Compras
        public ActionResult Index(string sortOrder, string currentFilter, string strBusqueda, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewBag.CategoriaSortParm = sortOrder == "Producto" ? "Producto_desc" : "Producto";
            ViewBag.SubtotalSortParm = sortOrder == "Subtotal" ? "Subtotal_desc" : "Subtotal";
            ViewBag.FechaSortParm = sortOrder == "Fecha" ? "Fecha_desc" : "Fecha";
            if (strBusqueda != null)
            {
                page = 1;
            }
            else
            {
                strBusqueda = currentFilter;
            }

            ViewBag.CurrentFilter = strBusqueda;
            var compras = db.Compras.AsEnumerable();
            if (!String.IsNullOrEmpty(strBusqueda))
            {
                compras = compras.Where(s => s.Cliente.Nombre.Contains(strBusqueda) || s.Producto.Nombre.Contains(strBusqueda));
            }
            switch (sortOrder)
            {
                case "nombre_desc":
                    compras = compras.OrderByDescending(s => s.Cliente.Nombre);
                    break;
                case "Producto":
                    compras = compras.OrderBy(s => s.Producto.Nombre);
                    break;
                case "Producto_desc":
                    compras = compras.OrderByDescending(s => s.Producto.Nombre);
                    break;
                case "Subtotal":
                    compras = compras.OrderBy(s => s.SubTotal);
                    break;
                case "Subtotal_desc":
                    compras = compras.OrderByDescending(s => s.SubTotal);
                    break;
                case "Fecha":
                    compras = compras.OrderBy(s => s.FechaCompra);
                    break;
                case "Fecha_desc":
                    compras = compras.OrderByDescending(s => s.FechaCompra);
                    break;
                default:
                    compras = compras.OrderBy(s => s.Cliente.Nombre);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(compras.ToPagedList(pageNumber, pageSize));
            //var compras = db.Compras.Include(c => c.Cliente).Include(c => c.Producto);
            //return View(compras.ToList());
        }


        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre");
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre");
            return View();
        }


        public ActionResult Confirmacion()
        {
            return View();
        }
        // POST: Compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompraID,ProductoID,ClienteID,SubTotal,FechaCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                int CompraID = db.Compras.Include(c => c.Cliente).Include(c => c.Producto).ToList().Count + 1;
                foreach (CarritoItem item in CarritoProductos.listaProductos)
                {
                    Compra unaCompra = new Compra();
                    unaCompra.ProductoID = item.ProductoID;
                    unaCompra.SubTotal = item.Subtotal;
                    unaCompra.FechaCompra = DateTime.Now;
                    unaCompra.ClienteID = compra.ClienteID;
                    unaCompra.Cliente = compra.Cliente;
                    unaCompra.Cantidad = item.Cantidad;
                    unaCompra.CompraID = CompraID;
                    db.Compras.Add(unaCompra);

                }
                    db.SaveChanges();
                CarritoProductos.listaProductos = new List<CarritoItem>();
                CarritoProductos.NombreCliente = db.Clientes.Find(compra.ClienteID).Nombre;
                return RedirectToAction("Confirmacion");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", compra.ClienteID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", compra.ProductoID);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", compra.ClienteID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", compra.ProductoID);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompraID,ProductoID,ClienteID,SubTotal,FechaCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", compra.ClienteID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", compra.ProductoID);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
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
