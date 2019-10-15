using System.Collections.Generic;

namespace ToDoList.Models
{
    public class Category
    {
        public Category()
        {
            this.Items = new HashSet<Item>(); //A HashSet is an unordered collection of unique elements. We create a HashSet of Items in the constructor to help avoid exceptions when no records exist in the "many" side of the relationship.
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; } //We declare Items as an instance of ICollection, a generic interface built into the .NET framework. We use ICollection specifically because Entity requires it.
    }
}