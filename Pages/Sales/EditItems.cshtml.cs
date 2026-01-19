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

namespace StitchWitches.Pages.Sales
{
    public class EditItemsModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public EditItemsModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;

        public SelectList ItemsInStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale =  await _context.Sale.FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            Sale = sale;

            var inStock = await _context.InventoryItem.Where(item => item.Quantity > 0).ToListAsync();
            ItemsInStock = new SelectList(inStock, nameof(InventoryItem.Id), nameof(InventoryItem.Name), null, nameof(InventoryItem.Category));
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

            _context.Attach(Sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(Sale.Id))
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

        private bool SaleExists(int id)
        {
            return _context.Sale.Any(e => e.Id == id);
        }
    }
}
