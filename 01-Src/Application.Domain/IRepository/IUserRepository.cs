using System;
using Application.Domain.Model;
using T.Application.Domain;

namespace Application.Domain.IRepository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}