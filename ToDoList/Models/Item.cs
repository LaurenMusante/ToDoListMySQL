using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Id { get; }
   

    public Item(string description)
    {
      Description = description;

    }
    
    public Item(string description, int id)
        {
            Description = description;
            Id = id;
        }

    public static List<Item> GetAll()
    {
     List<Item> allItems = new List<Item> { };
        MySqlConnection conn = DB.Connection();
        conn.Open(); // opens DB connection
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM items;"; //this is b/c we are defining our GetAll() method, and this is what we want it to do (get all of our items from the database)
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        // this line is responsible for reading the data returned from the database. it also casts it as a mysql data-reader object.
        while (rdr.Read())
        {
            int itemId = rdr.GetInt32(0);
            string itemDescription = rdr.GetString(1);
            Item newItem = new Item(itemDescription, itemId);
            allItems.Add(newItem);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allItems;
    }

    public static void ClearAll()
    {

    }

    public static Item Find(int searchId)
    {
      // Temporarily returning placeholder item to get beyond compiler errors until we refactor to work with database.
    Item placeholderItem = new Item("placeholder item");
    return placeholderItem;
    }

  }
}