using System;
using System.ComponentModel.DataAnnotations;
using T.Application.Domain;

namespace Applications.Domains.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class User : EntityBase<User>, IDelete, IKey<Guid>
    {
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="id"></param>
        public User(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public User() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 用户
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}