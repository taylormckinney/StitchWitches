using System.ComponentModel.DataAnnotations;

namespace StitchWitches.Models
{
    public enum MakerEnum
    {
        [Display(Name = "Made by Taylor")]
        Taylor,
        [Display(Name = "Made by Megan")]
        Megan,
        [Display(Name = "Made by Landis")]
        Landis
    }
}
