﻿using System;
using Application.Data.DbContext;
using Application.Domain.IRepository;
using Application.Domain.Model;

namespace Application.Data.Repository
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(TestDbContext myDbContext, IUserRepository userRepository) : base(myDbContext)
        {
        }
    }
}