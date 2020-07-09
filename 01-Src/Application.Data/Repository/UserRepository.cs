using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Applications.Data.DbContext;
using Applications.Domains.IRepository;
using Applications.Domains.Model;
using Applications.Domains.Query;
using Microsoft.EntityFrameworkCore;
using T.Application.Data.Repository;

namespace Applications.Data.Repository
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(TestDbContext myDbContext) : base(myDbContext)
        {
        }

        public async Task AddUser(User model)
        {
            model.Init();
            await AddAsync(model);
        }

        public IQueryable<User> PageQuery(UserQuery query)
        {
            return FindAsync(x=>x.Age==query.Age).OrderByDescending(x=>x.Age);
        }

        public async Task<IEnumerable<User>> Query(UserQuery query)
        {
            var query2 = MyDbContext.Set<User>().AsQueryable();

            var t = query.GetType();
             foreach (var filed in t.GetFields())
             {
                 var s = filed.GetValue(query);
                 if (s!=null)
                 {
                    
                 }
             }

             return  await FindAsync(x => x.Age == query.Age).OrderBy(x => x.Age).ToListAsync();
        }
    }
}