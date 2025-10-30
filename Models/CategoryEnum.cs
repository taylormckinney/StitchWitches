using System.ComponentModel.DataAnnotations;

namespace StitchWitches.Models
{
    public enum CategoryEnum
    {
        Clothing,
        [Display(Name = "Wearable Accessories")]
        WearableAccessories,
        Keychains,
        [Display(Name = "Car Accessories")]
        CarAccessories,
        [Display(Name = "Home Decor")]
        HomeDecor,
        [Display(Name = "Stuffed Animals")]
        StuffedAnimals,
        [Display(Name = "Cat Toys")]
        CatToys,
        [Display(Name = "Bottle and Can Coozies")]
        Coozies,
        Bags,
        Miscellaneous
    }
}
