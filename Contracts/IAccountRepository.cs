using Entities.DataTransferObjects;
using Entities.Models;

namespace Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> AccountsByOwner(Guid ownerId);

        PagedList<Account> GetAccountsByOwner(Guid ownerId, AccountParameters parameters);
    }
}
