using System.ComponentModel.DataAnnotations;

namespace BleemsTest.Models
{
    public class Category
    {
        [Key] public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
