using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Products
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Product Picture ")]
        public string Image { get; set;}

        [NotMapped]
        [Display(Name = "Product Picture ")]
        public IFormFile ImageFile { get; set; }


        [Display(Name = "Product Color ")]
        public string ProductColor { get; set;}
        [Required]
        [Display(Name = "Product Sizes ")]
        public string ProductSize { get; set;}

        [Display(Name = "Product Type ")]
        [Required]
        public int ProductTypeId { get; set;}
        [ForeignKey("ProductTypeId")]
        public virtual ProductTypes ProductTypes { get; set;}
    }
}
