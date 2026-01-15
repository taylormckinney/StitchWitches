using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages.Sales
{
    public class StartModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public StartModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }
        public SelectList ItemsInStock { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var inStock = await _context.InventoryItem.Where(item => item.Quantity > 0).ToListAsync();
            ItemsInStock = new SelectList(inStock, nameof(InventoryItem.Id),nameof(InventoryItem.Name), null, nameof(InventoryItem.Category));
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;
        [BindProperty]
        public int[] SelectedItems { get; set; }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Sale.SaleDate = DateTime.Now;
            //foreach (var itemId in SelectedItems)
            //{
            //    Sale.ItemsSold.Add(itemId);

            //}

            _context.Sale.Add(Sale);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Finalize", new {id=Sale.Id});
        }
    }
}
