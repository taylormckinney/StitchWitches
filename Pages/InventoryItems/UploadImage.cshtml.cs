using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages.InventoryItems
{
    public class UploadImageModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;
        private readonly IWebHostEnvironment _environment;

        public UploadImageModel(StitchWitches.Data.StitchWitchesContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = default!;
        
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

        
       
        [BindProperty]
        public IFormFile ImgUpload { get; set; }
        public async Task<IActionResult> OnPostUploadAsync()
        {
            var file = Path.Combine(_environment.WebRootPath, "images", ImgUpload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await ImgUpload.CopyToAsync(fileStream);
            }
            InventoryItem.ImagePath = file;
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
