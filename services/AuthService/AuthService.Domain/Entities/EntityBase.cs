using AuthService.Domain.Interfaces;

namespace AuthService.Domain.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        protected EntityBase() { }
    }
}
