using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Applications.Domains.Query;
using Applications.Service.Dto;
using T.Application.Service;

namespace Applications.Service.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService : IApplicationService<UserDto, UserQuery>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task AddUser(UserDto dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<MyPagedList<UserDto>> PageQuery(UserQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> Query(UserQuery query);
    }
}