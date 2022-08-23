using AppDev.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppDev.Areas.StoreOwner.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(20)]
        public string? Isbn { get; set; } = null!;

        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public ICollection<Category> Categories { get; set; } = null!;
    }
}