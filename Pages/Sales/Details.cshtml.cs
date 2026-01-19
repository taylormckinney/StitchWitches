using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages.Sales
{
    public class DetailsModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public DetailsModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }

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

            if (sale is not null)
            {
                Sale = sale;
                if (Sale.ItemsSold is not null && Sale.ItemsSold.Count > 0)
                {
                    foreach(var itemId in Sale.ItemsSold)
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

            return NotFound();
        }
    }
}
