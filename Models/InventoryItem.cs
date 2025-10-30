using Microsoft.AspNetCore.Mvc;
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
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        public MakerEnum CreatedBy { get; set; }
        public CategoryEnum Category { get; set; }
        public string? ImagePath { get; set; }

    }

  
}
