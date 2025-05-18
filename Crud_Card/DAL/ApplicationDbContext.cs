using Crud_Card.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud_Card.DAL
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } 

        public DbSet<Card> Cards { get; set; }

      
    }
}
