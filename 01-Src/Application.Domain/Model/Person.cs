using System;
using T.Application.Domain;

namespace Applications.Domains.Model
{
    public class Person : EntityBase<Person>, IDelete, IKey<Guid>
    {
        public Person(Guid id) : base(id)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}