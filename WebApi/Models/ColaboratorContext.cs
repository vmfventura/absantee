using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain    
{
    public class ColaboratorContext: DbContext
    {
        public ColaboratorContext(DbContextOptions<ColaboratorContext> options)
        : base(options)
        {
        }

        public DbSet<Colaborator> Colaborators { get; set; } = null!;
    }
}