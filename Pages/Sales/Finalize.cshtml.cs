using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using StitchWitches.Data;
using StitchWitches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StitchWitches.Pages.Sales
{
    public class FinalizeModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public FinalizeModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;
        [BindProperty]
        public IList<InventoryItem> SoldItems { get; set; } = new List<InventoryItem>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.FirstOrDefaultAsync(m => m.Id == id);

            if (sale == null)
            {
                return NotFound();
            }
            Sale = sale;
            if (Sale.ItemsSold is not null && Sale.ItemsSold.Count > 0)
            {
                foreach (var itemId in Sale.ItemsSold)
                {
                    var item = await _context.InventoryItem.FindAsync(itemId);
                    if (item is not null)
                    {
                        SoldItems.Add(item);
                    }
                }
            }

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

            var dbItem = await _context.Sale.FindAsync(Sale.Id);
            if (await TryUpdateModelAsync<Sale>(
                dbItem,
                $"Sale",
                x => x.SaleDate, x => x.Total, x => x.PaymentMethod,x => x.Notes, x=>x.Market))
            {
                _context.Attach(dbItem).State = EntityState.Modified;
            }


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
