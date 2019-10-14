using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Id { get; set; }
   

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

  // SQL PATTERN: open a sql connection, write a command, read the command in a while loop, close the connection
    public static void ClearAll()
    {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE items;";
       cmd.ExecuteNonQuery();
       conn.Close();
     if (conn != null)
     {
      conn.Dispose();
     }
      
    }

 public static Item Find(int id)
{
    // We open a connection.
    MySqlConnection conn = DB.Connection();
    conn.Open();

    // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";

    // We have to use parameter placeholders (@thisId) and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
    MySqlParameter thisId = new MySqlParameter();
    thisId.ParameterName = "@thisId";
    thisId.Value = id;
    cmd.Parameters.Add(thisId);

    // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    int itemId = 0;
    string itemDescription = "";
    while (rdr.Read())
    {
      itemId = rdr.GetInt32(0);
      itemDescription = rdr.GetString(1);
    }
    Item foundItem= new Item(itemDescription, itemId);

    // We close the connection.
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return foundItem;
}
    
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";
      //  the parameter placeholder (@ItemDescription) is similar to how we store and conceal API keys. it hides potentially sensitive information, but allows us to still use its value while communicating to the database.
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this.Description;
      cmd.Parameters.Add(description);    
      cmd.ExecuteNonQuery();
      // this line executes the command we defined above in cmd.commandtext
     Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
        {
          conn.Dispose();
        }
    }

  }
}