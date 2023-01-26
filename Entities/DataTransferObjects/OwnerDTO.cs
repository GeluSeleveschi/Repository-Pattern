using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OwnerDTO
    {
        public Guid OwnerId { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
    }

    public class OwnerWithAccountsDTO : OwnerDTO
    {
        public IEnumerable<AccountDTO>? Accounts { get; set; }
    }
}
