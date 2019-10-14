using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;

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
      List<Item> model = _db.Items.ToList();
      return View(model);
    }


    public ActionResult Create()
    {
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
  }
}