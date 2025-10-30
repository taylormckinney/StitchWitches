using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages.InventoryItems
{
    public class DeleteModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public DeleteModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryitem = await _context.InventoryItem.FirstOrDefaultAsync(m => m.Id == id);

            if (inventoryitem is not null)
            {
                InventoryItem = inventoryitem;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryitem = await _context.InventoryItem.FindAsync(id);
            if (inventoryitem != null)
            {
                InventoryItem = inventoryitem;
                if(InventoryItem.ImagePath is not null) //also delete image file when deleting item
                {
                    var imagePath = Path.Combine("wwwroot/images", InventoryItem.ImagePath);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _context.InventoryItem.Remove(InventoryItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
