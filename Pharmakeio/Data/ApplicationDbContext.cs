using Microsoft.EntityFrameworkCore;
using Pharmakeio.Models;

namespace Pharmakeio.Data
{
    public class ApplicationDbContext : DbContext

    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<PharmaceuticalDepartment> PharmaceuticalDepartments { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
