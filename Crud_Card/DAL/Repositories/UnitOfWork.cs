using Crud_Card.DAL.Entities;
using Crud_Card.DAL.Interfaces;

namespace Crud_Card.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;


        public IRepository<Card> Cards { get; }

        public UnitOfWork(ApplicationDbContext context )
        {
            _context = context;
            Cards=new IRespository<Card>(_context);
        }
        public async Task<int> CreateAsync()=> await _context.SaveChangesAsync();
        
    }
}
