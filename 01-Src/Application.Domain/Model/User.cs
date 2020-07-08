using System;

namespace Application.Domain.Model
{
    public class User : EntityBase<User>, IDelete, IKey<Guid>
    {
        public User(Guid id) : base(id)
        {
        }

        public User() : this(Guid.Empty)
        {
        }

        public bool IsDeleted { get; set; }
    }
}