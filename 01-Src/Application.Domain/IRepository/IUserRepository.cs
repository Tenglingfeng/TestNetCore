using System;
using Application.Domain.Model;

namespace Application.Domain.IRepository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}