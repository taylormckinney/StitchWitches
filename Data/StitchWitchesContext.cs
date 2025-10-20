using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StitchWitches.Models;

namespace StitchWitches.Data
{
    public class StitchWitchesContext : DbContext
    {
        public StitchWitchesContext (DbContextOptions<StitchWitchesContext> options)
            : base(options)
        {
        }

        public DbSet<StitchWitches.Models.InventoryItem> InventoryItem { get; set; } = default!;
    }
}
