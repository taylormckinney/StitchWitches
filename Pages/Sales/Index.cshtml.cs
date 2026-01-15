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
    public class IndexModel : PageModel
    {
        private readonly StitchWitches.Data.StitchWitchesContext _context;

        public IndexModel(StitchWitches.Data.StitchWitchesContext context)
        {
            _context = context;
        }

        public IList<Sale> Sale { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Sale = await _context.Sale.ToListAsync();
        }
    }
}
