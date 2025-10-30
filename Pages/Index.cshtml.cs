using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using StitchWitches.Models;
using System.Threading.Tasks;

namespace StitchWitches.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly StitchWitches.Data.StitchWitchesContext _context;

    public IndexModel(ILogger<IndexModel> logger, Data.StitchWitchesContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IList<InventoryItem> Items { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Items = await _context.InventoryItem.ToListAsync();
    }
}
