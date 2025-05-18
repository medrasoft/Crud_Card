using Crud_Card.DAL.Entities;

namespace Crud_Card.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Card> Cards { get; }

        Task<int> CreateAsync();
    }
}
