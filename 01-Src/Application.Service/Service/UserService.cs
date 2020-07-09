using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Data.DbContext;
using Applications.Domains.IRepository;
using Applications.Domains.Model;
using Applications.Domains.Query;
using Applications.Service.Dto;
using Applications.Service.Impl;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using T.Application.Service;

namespace Applications.Service.Service
{
    public class UserService : ApplicationServiceBase<User, UserDto, Guid, UserQuery>, IUserService
    {
        public UserService(TestDbContext myDbContext, IUserRepository userRepository) : base(userRepository, myDbContext)
        {
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository;

        protected override UserDto ToDto(User entity)
        {
            return entity.ToDto();
        }

        protected override User ToEntity(UserDto dto)
        {
            return dto.ToEntity();
        }

        public async Task AddUser(UserDto dto)
        {
            await UserRepository.AddUser(dto.ToEntity());
            await MyDbContext.SaveChangesAsync();
        }

        public async Task<MyPagedList<UserDto>> PageQuery(UserQuery query)
        {
           return await Task.FromResult(UserRepository.PageQuery(query).ToPagedList(query.PageIndex, query.PageSize).ConvertTo(x => x.ToDto()));
        }

        public async Task<IEnumerable<UserDto>> Query(UserQuery query)
        {
          var result =  await UserRepository.Query(query);
          return result.Select(x => x.ToDto());
        }
    }
}