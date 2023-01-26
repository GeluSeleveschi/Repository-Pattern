using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private readonly ISortHelper<Account> _sortHelper;
        private readonly IDataShaper<Account> _dataShaper;

        public AccountRepository(RepositoryContext repositoryContext, ISortHelper<Account> sortHelper, IDataShaper<Account> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();
        }

        public PagedList<Account> GetAccountsByOwner(Guid ownerId, AccountParameters parameters)
        {
            var accounts = FindByCondition(a => a.OwnerId.Equals(ownerId));

            return PagedList<Account>.ToPagedList(accounts, parameters.PageNumber, parameters.PageSize);
        }
    }
}
