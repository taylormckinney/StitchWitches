using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StitchWitches.Data;
using StitchWitches.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StitchWitches.Pages.InventoryItems
{
    public class IndexModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public IndexModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }

        public IList<InventoryItem> InventoryItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            InventoryItem = await _context.InventoryItem.ToListAsync();
        }

    }
}
