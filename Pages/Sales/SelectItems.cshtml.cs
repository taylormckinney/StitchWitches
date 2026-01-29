using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages.Sales
{
    public class SelectItemsModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public SelectItemsModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public Sale Sale { get; set; } = default!;
        [BindProperty]
        public List<int> Selected { get; set; } = new List<int>();
        [BindProperty]
        public IList<IList<InventoryItem>> CategoryLists { get; set; } = new List<IList<InventoryItem>>();
        public IList<InventoryItem> CategoryStock { get; set; } = default!;


        public async Task OnGetAsync()
        {
            var categories = Enum.GetValues<CategoryEnum>().ToList();
            foreach (CategoryEnum cat in categories)
            {
                CategoryStock = await _context.InventoryItem.Where(item => item.Quantity > 0 && item.Category == cat).ToListAsync();

                if (CategoryStock is not null && CategoryStock.Count > 0)
                {
                    CategoryLists.Add(CategoryStock);
                }

            }

        }
        //not actually hitting here... either need to fix route on finalize page 
        //or rethink.. only one OnGetAsync, if id present, load existing sale??? create/edit in one? 
        public async Task<IActionResult> OnGetExistingAsync(int? id)
        {
            await OnGetAsync();
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
            Selected = [.. Sale.ItemsSold];
            return Page();
        }




        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //adding selected items to sale
            foreach (var itemId in Selected)
            {
                Sale.ItemsSold.Add(itemId);
            }
            //date and subtotal calculated: 
            Sale.SaleDate = DateTime.Now;
            decimal subtotal = 0.0m;
            foreach (var itemId in Sale.ItemsSold)
            {
                var item = await _context.InventoryItem.FindAsync(itemId);
                subtotal += item.Price;
            }
            Sale.Total = subtotal;


            if(Sale.Id == 0)
            {
                _context.Sale.Add(Sale);

                
            }
            else
            {
                _context.Attach(Sale).State = EntityState.Modified;
            }
                
            await _context.SaveChangesAsync();





            return RedirectToPage("./Finalize", new { id = Sale.Id });
        }
    }
}
