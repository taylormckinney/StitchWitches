using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;    
namespace StitchWitches.Models
{
   
    public class InventoryItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
        [BindProperty]
        public MakerEnum CreatedBy { get; set; }
    }
}
