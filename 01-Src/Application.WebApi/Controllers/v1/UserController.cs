using System.Threading.Tasks;
using Applications.Domains.Query;
using Applications.Service.Dto;
using Applications.Service.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using T.Application.IdentityServer4;

namespace Applications.WebApi.Controllers.v1
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        private readonly IUserService _userService;

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto dto)
        {
            await _userService.AddUser(dto);
            return Ok();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PageQuery([FromQuery]UserQuery query)
        {
           var result =  await _userService.PageQuery(query);
            return Ok(result);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Query([FromQuery]UserQuery query)
        {
            var result = await _userService.Query(query);
            return Ok(result);
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetToken()
        {
            var result = await IdentityServerHelper.GetToken("client", "secret", "scope1");
            return Ok(result);
        }


    }
}