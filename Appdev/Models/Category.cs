using System.ComponentModel.DataAnnotations;

namespace AppDev.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = false;
    }
}
