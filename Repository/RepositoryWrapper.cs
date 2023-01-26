using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IOwnerRepository _ownerRepo;
        private IAccountRepository _accountRepo;
        private ISortHelper<Owner> _ownerSortHelper;
        private ISortHelper<Account> _accountSortHelper;
        private IDataShaper<Owner> _ownerDataShaper;
        private IDataShaper<Account> _accountDataShaper;

        public IOwnerRepository Owner
        {
            get
            {
                if (_ownerRepo == null)
                {
                    _ownerRepo = new OwnerRepository(_repositoryContext, _ownerSortHelper, _ownerDataShaper);
                }

                return _ownerRepo;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_accountRepo == null)
                {
                    _accountRepo = new AccountRepository(_repositoryContext, _accountSortHelper, _accountDataShaper);
                }

                return _accountRepo;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext, ISortHelper<Owner> ownerSortHelper, ISortHelper<Account> accountSortHelper, IDataShaper<Owner> ownerDataShaper, IDataShaper<Account> accountDataShaper)
        {
            _repositoryContext = repositoryContext;
            _ownerSortHelper = ownerSortHelper;
            _accountSortHelper = accountSortHelper;
            _ownerDataShaper = ownerDataShaper;
            _accountDataShaper = accountDataShaper;
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
