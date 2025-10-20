namespace StitchWitches.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public enum Maker { Taylor, Megan, Landis}
        public Maker CreatedBy { get; set; }
    }
}
