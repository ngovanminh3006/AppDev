using System.ComponentModel.DataAnnotations;

namespace AppDev.ViewModels
{
    public class NewCategoryRequestCreateViewModel
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}
