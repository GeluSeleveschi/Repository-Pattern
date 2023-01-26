using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        private readonly ISortHelper<Owner> _sortHelper;
        private readonly IDataShaper<Owner> _dataShaper;
        public OwnerRepository(RepositoryContext repositoryContext, ISortHelper<Owner> sortHelper, IDataShaper<Owner> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public PagedList<ExpandoObject> GetAllOwners(OwnerParameters ownerParameters)
        {
            var owners = FindByCondition(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth
                                && o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth).OrderBy(x => x.Name).AsQueryable();

            SearchByName(ref owners, ownerParameters.Name);

            _sortHelper.ApplySort(owners, ownerParameters.OrderBy);
            var shapedOwners = _dataShaper.ShapeData(owners, ownerParameters.Fields);

            return PagedList<ExpandoObject>.ToPagedList(shapedOwners,
                ownerParameters.PageNumber,
                ownerParameters.PageSize);
        }

        public Owner GetOwnerById(Guid ownerId)
        {
            return FindByCondition(owner => owner.OwnerId.Equals(ownerId)).FirstOrDefault();
        }

        public Owner GetOwnerWithDetails(Guid ownerId)
        {
            return FindByCondition(owner => owner.OwnerId.Equals(ownerId)).Include(ac => ac.Accounts).FirstOrDefault();
        }

        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }

        private void SearchByName(ref IQueryable<Owner> owners, string ownerName)
        {
            if (!owners.Any() || string.IsNullOrWhiteSpace(ownerName))
                return;

            owners = owners.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
        }

        public ExpandoObject GetOwnerById(Guid ownerId, string fields)
        {
            var owner = FindByCondition(owner => owner.OwnerId.Equals(ownerId)).DefaultIfEmpty(new Owner()).FirstOrDefault();

            return _dataShaper.ShapeData(owner, fields);
        }
    }
}
