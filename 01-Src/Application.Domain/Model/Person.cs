using System;
using T.Application.Domain;

namespace Application.Domain.Model
{
    public class Person:EntityBase<Person>,IDelete,IKey<Guid>
    {
        public Person(Guid id) : base(id)
        {
        }

        public bool IsDeleted { get; set; }
    }
}
