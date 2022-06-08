using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TutorialService.Models;

namespace TutorialService.Data
{
    public class TutorialServiceContext : DbContext
    {
        public TutorialServiceContext (DbContextOptions<TutorialServiceContext> options)
            : base(options)
        {
        }
        public DbSet<Example>? Example { get; set; }
        public DbSet<Group>? Group { get; set; }
        public DbSet<Enrollment>? Enrollment { get; set; }
    }
}
