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
    public class ProductoesController : Controller
    {
        private TiendaContext db = new TiendaContext();

        // GET: Productoes
        public ActionResult Index(string sortOrder, string currentFilter, string strBusqueda, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewBag.CategoriaSortParm = sortOrder == "Categoria" ? "Categoria_desc" : "Categoria";
            ViewBag.PrecioSortParm = sortOrder == "Precio" ? "Precio_desc" : "Precio";
            if (strBusqueda != null)
            {
                page = 1;
            }
            else
            {
                strBusqueda = currentFilter;
            }

            ViewBag.CurrentFilter = strBusqueda;
            var productos = db.Productos.AsEnumerable();
            if (!String.IsNullOrEmpty(strBusqueda))
            {
                productos = productos.Where(s => s.Nombre.Contains(strBusqueda) || s.Categoria.Nombre.Contains(strBusqueda));
            }
            switch (sortOrder)
            {
                case "nombre_desc":
                    productos = productos.OrderByDescending(s => s.Nombre);
                    break;
                case "Categoria":
                    productos = productos.OrderBy(s => s.Categoria.Nombre);
                    break;
                case "Categoria_desc":
                    productos = productos.OrderByDescending(s => s.Categoria.Nombre);
                    break;
                case "Precio":
                    productos = productos.OrderBy(s => s.Precio);
                    break;
                case "Precio_desc":
                    productos = productos.OrderByDescending(s => s.Precio);
                    break;
                default:
                    productos = productos.OrderBy(s => s.Nombre);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(productos.ToPagedList(pageNumber, pageSize));
            //var productos = db.Productos.Include(p => p.Categoria);
            //return View(productos.ToList());
        }
        //Método para agregar el producto al carrito
        [HttpPost]
        public ActionResult AgregarCarrito(FormCollection form)
        {
            Producto producto = db.Productos.Find(int.Parse(Request["item.ProductoID"].ToString()));
            //Instanciamos un objeto de articulo de carrito y le damos sus propiedades correspondientes
            CarritoItem item = new CarritoItem();
            item.CarritoID = CarritoProductos.listaProductos.Count + 1;
            item.Producto = producto;
            item.ProductoID = producto.ProductoID;
            item.Cantidad = int.Parse(Request["Cantidad"].ToString());
            item.CalcularSubtotal();

            //Agregamos el artículo al carrito(lista)
            CarritoProductos.listaProductos.Add(item);

           
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoID,Nombre,Precio,Descripcion,RutaImagen,CategoriaID")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", producto.CategoriaID);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", producto.CategoriaID);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoID,Nombre,Precio,Descripcion,RutaImagen,CategoriaID")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", producto.CategoriaID);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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
