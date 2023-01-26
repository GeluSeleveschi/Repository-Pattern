namespace Entities.DataTransferObjects
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string? AccountType { get; set; }
    }
}
