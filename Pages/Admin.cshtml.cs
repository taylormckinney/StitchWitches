using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StitchWitches.Data;
using StitchWitches.Models;

namespace StitchWitches.Pages
{
    [Authorize]
    public class AdminModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public AdminModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<InventoryItem> InventoryItems { get; set; } = default!;

        public async Task OnGetAsync()
        {
            InventoryItems = await _context.InventoryItem.ToListAsync();
        }
        
        public async Task<IActionResult> OnPostSaveAllAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            for (int i = 0; i < InventoryItems.Count; i++)
            {
                var dbItem = await _context.InventoryItem.FindAsync(InventoryItems[i].Id);
                if(await TryUpdateModelAsync<InventoryItem>(
                    dbItem,
                    $"InventoryItems[{i}]",
                    x => x.Quantity, x => x.Price, x => x.SellCount))
                {
                    _context.Attach(dbItem).State = EntityState.Modified;
                }
            }
           
            await _context.SaveChangesAsync();
            return RedirectToPage("./Admin");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            int index = int.Parse(HttpContext.Request.Form["SaveSingle"]);
            var dbItem = await _context.InventoryItem.FindAsync(InventoryItems[index].Id);
            if (await TryUpdateModelAsync<InventoryItem>(
                dbItem,
                $"InventoryItems[{index}]",
                x => x.Quantity, x => x.Price, x => x.SellCount))
            {
                _context.Attach(dbItem).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Admin");
        }
        private bool InventoryItemExists(int id)
        {
            return _context.InventoryItem.Any(e => e.Id == id);
        }

    }
}
