using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages.InventoryItems
{
    public class EditModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(StitchWitches.Data.StitchWitchesContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Upload Image")]
        public IFormFile? ImgUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryitem =  await _context.InventoryItem.FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryitem == null)
            {
                return NotFound();
            }
            InventoryItem = inventoryitem;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(InventoryItem).State = EntityState.Modified;
            if (ImgUpload is not null)
            {
                var file = Path.Combine(_environment.WebRootPath, "images", ImgUpload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await ImgUpload.CopyToAsync(fileStream);
                }
                InventoryItem.ImagePath = ImgUpload.FileName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(InventoryItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InventoryItemExists(int id)
        {
            return _context.InventoryItem.Any(e => e.Id == id);
        }
    }
}
