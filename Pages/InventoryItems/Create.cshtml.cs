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

        public CreateModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InventoryItem.Add(InventoryItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
