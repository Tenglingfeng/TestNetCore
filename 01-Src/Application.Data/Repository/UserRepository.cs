using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public  IQueryable<User> PageQuery(UserQuery query)
        {
            QueryBase<User> query1 = new QueryBase<User>();
            return FindAsync(query1.GetExpression()).OrderByDescending(x => x.Id);
        }

        public async Task<IEnumerable<User>> Query(UserQuery query)
        {
            QueryBase<User> query1 = new QueryBase<User>()
                .Filter(x => x.Age > query.Age)
                .Filter(x => x.Name.Contains(query.Name));

            var result = await FindAsync(query1.GetExpression()).OrderByDescending(x=>x.Id).ToListAsync();
            return result;
        }

        //public IQueryable
    }

    public class QueryBase<T>
    {
        public Expression<Func<T, bool>> Expression = t => true;

        public QueryBase<T> Filter(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new Exception("条件不能为空");
            }

            if (GetCriteriaCount(expression) > 1)
            {
                throw new Exception("一次只能添加一个条件");
            }

            var result = GetValue(expression);
            if (string.IsNullOrWhiteSpace((result == null) ? string.Empty : result.ToString().Trim()))
            {
                return this;
            }
            Expression = Expression.And(expression);
            return this;
        }

        //
        // 摘要:
        //     /// 安全转换为字符串，去除两端空格，当值为null时返回"" ///
        //
        // 参数:
        //   input:
        //     输入值

        public Expression<Func<T, bool>> GetExpression()
        {
            return Expression;
        }

        //
        // 摘要:
        //     /// 获取谓词条件的个数 ///
        //
        // 参数:
        //   expression:
        //     谓词表达式,范例：t => t.Name == "A"
        public static int GetCriteriaCount(LambdaExpression expression)
        {
            if (expression == null)
            {
                return 0;
            }
            string text = expression.ToString().Replace("AndAlso", "|").Replace("OrElse", "|");
            return text.Split('|').Count();
        }

        //
        // 摘要:
        //     /// 获取值,范例：t => t.Name == "A",返回 A ///
        //
        // 参数:
        //   expression:
        //     表达式,范例：t => t.Name == "A"
        public static object GetValue(Expression expression)
        {
            if (expression == null)
            {
                return null;
            }
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetValue(((LambdaExpression)expression).Body);

                case ExpressionType.Convert:
                    return GetValue(((UnaryExpression)expression).Operand);

                case ExpressionType.Equal:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.NotEqual:
                    return GetValue(((BinaryExpression)expression).Right);

                case ExpressionType.Call:
                    return GetMethodCallExpressionValue(expression);

                case ExpressionType.MemberAccess:
                    return GetMemberValue((MemberExpression)expression);

                case ExpressionType.Constant:
                    return GetConstantExpressionValue(expression);

                default:
                    return null;
            }
        }

        //
        // 摘要:
        //     /// 获取方法调用表达式的值 ///
        private static object GetMethodCallExpressionValue(Expression expression)
        {
            MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
            object value = GetValue(methodCallExpression.Arguments.FirstOrDefault());
            if (value != null)
            {
                return value;
            }
            return GetValue(methodCallExpression.Object);
        }

        //
        // 摘要:
        //     /// 获取属性表达式的值 ///
        private static object GetMemberValue(MemberExpression expression)
        {
            if (expression == null)
            {
                return null;
            }
            FieldInfo fieldInfo = expression.Member as FieldInfo;
            if (fieldInfo != null)
            {
                object constantExpressionValue = GetConstantExpressionValue(expression.Expression);
                return fieldInfo.GetValue(constantExpressionValue);
            }
            PropertyInfo propertyInfo = expression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                return null;
            }
            if (expression.Expression == null)
            {
                return propertyInfo.GetValue(null);
            }
            object memberValue = GetMemberValue(expression.Expression as MemberExpression);
            if (memberValue == null)
            {
                return null;
            }
            return propertyInfo.GetValue(memberValue);
        }

        //
        // 摘要:
        //     /// 获取常量表达式的值 ///
        private static object GetConstantExpressionValue(Expression expression)
        {
            ConstantExpression constantExpression = (ConstantExpression)expression;
            return constantExpression.Value;
        }
    }
}