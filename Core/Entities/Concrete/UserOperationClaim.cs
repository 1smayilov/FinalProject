namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        // Hansı istifaəçidə hansı rollar var 
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
