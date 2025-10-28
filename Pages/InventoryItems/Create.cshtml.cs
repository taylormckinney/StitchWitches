using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages.InventoryItems
{
    public class CreateModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(StitchWitches.Data.StitchWitchesContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = default!;

        [BindProperty]
        public IFormFile ImgUpload { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ImgUpload is not null)
            {
                var file = Path.Combine(_environment.WebRootPath, "images", ImgUpload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await ImgUpload.CopyToAsync(fileStream);
                }
                InventoryItem.ImagePath = ImgUpload.FileName;
            }

            _context.InventoryItem.Add(InventoryItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
 
    }
}
