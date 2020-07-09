using System;
using T.Application.Service;

namespace Applications.Service.Dto
{

    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto : DtoBase
    {
        
        /// <summary>
        /// 姓名
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}