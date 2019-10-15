using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ToDoListContext _db;

        public ItemsController(ToDoListContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Item> model = _db.Items.Include(items => items.Category).ToList(); //We can utilize eager loading by using Entity's built-in Include() methodThis basically states the following: for each Item in the database, include the Category it belongs to and then put all the Items into list.
            return View(model);
        }


        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            //^This is our way of communicating to the database, giving it the id that was passed in and telling the database to give us the item in the database that has this id. In this line, "items" is not a special name.
            return View(thisItem);
        }
        public ActionResult Edit(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            _db.Items.Remove(thisItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}