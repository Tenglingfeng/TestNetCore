using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Domains.Model;
using Applications.Domains.Query;
using T.Application.Domain;

namespace Applications.Domains.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddUser(User model);


        IQueryable<User> PageQuery(UserQuery  query);
        Task<IEnumerable<User>> Query(UserQuery query);
    }
}