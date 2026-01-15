using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StitchWitches.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string? Market { get; set; }
        [Display(Name = "Date of Sale")]
        [DataType(DataType.DateTime)]
        public DateTime? SaleDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [Display(Name = "Items Sold")]
        [Required]
        public IList<int> ItemsSold { get; set; } = [];
        [Display(Name = "Payment Method")]
        public PaymentEnum PaymentMethod { get; set; }
    }

}
