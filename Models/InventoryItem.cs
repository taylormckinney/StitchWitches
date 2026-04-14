using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StitchWitches.Models
{
   
    public class InventoryItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        [Display(Name="In Stock")]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name="Crocheter")]
        public MakerEnum CreatedBy { get; set; }
        public CategoryEnum Category { get; set; }
        public string? ImagePath { get; set; }
        [Display(Name="Times Sold")]
        public int SellCount { get; set; }

    }

  
}
