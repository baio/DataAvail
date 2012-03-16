using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Reflection;
using System.Web;
using Microsoft.Data.Services.Toolkit.QueryModel;
using System.Linq.Expressions;
using System.Data.Services.Common;

namespace DataAvail.DataService.Provider
{
    public abstract class Repository<E, T>
        where E : class
        where T : class
    {

        protected abstract ObjectContext Context { get; }

        public Repository()
        { 
        
        }

        protected IQueryable<E> Queryable
        {
            get
            {
                var pi = Context.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.PropertyType == typeof(ObjectSet<E>) && p.CanRead && !p.CanWrite).Single();

                return (IQueryable<E>)pi.GetValue(Context, null);
            }
        }

        private static string[] GetExpands()
        {
            var queryString = HttpContext.Current.Request.QueryString;

            if (queryString["$expand"] != null)
            {
                return queryString["$expand"].Split(',');
            }
            else
            {
                return new string[0];
            }
        }

        public T GetOne(string id)
        {
            var q = DataAvail.LinqMapper.Mapper.Map<E, T>(Queryable, GetExpands());

            q = FilterByKey(q, id);

            return q.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(ODataQueryOperation operation)
        {
            var q = DataAvail.LinqMapper.Mapper.Map<E, T>(Queryable, GetExpands());

            if (operation.FilterExpression != null)
            {
                var expr = (System.Linq.Expressions.Expression<System.Func<T, bool>>)((UnaryExpression)operation.FilterExpression).Operand;

                q = q.Where(expr);
            }

            if (operation.TopCount > 0)
            {
                q = q.Take(operation.TopCount);
            }

            if (operation.SkipCount > 0)
            {
                q = q.Take(operation.SkipCount);
            }

            return q;
        }

        private static IQueryable<T> FilterByKey<T>(IQueryable<T> Queryable, string KeyValue)
        {
            var type = typeof(T);

            var keyFieldName = type.GetCustomAttributes(false).OfType<DataServiceKeyAttribute>().SingleOrDefault().KeyNames[0];

            var keyField = (PropertyInfo)type.GetMember(keyFieldName)[0];

            var key = int.Parse(KeyValue);

            ParameterExpression prm = Expression.Parameter(typeof(T), "x");
            MemberExpression member = Expression.MakeMemberAccess(prm, keyField);
            ConstantExpression idParam = Expression.Constant(key, keyField.PropertyType);
            BinaryExpression expr = Expression.Equal(member, idParam);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(expr, new ParameterExpression[] { prm });

            return Queryable.Where(lambda);
        }


        /*
        public IEnumerable<T> GetAllByForeignKey(ODataQueryOperation operation, string FKFieldName, string id)
        {
            return GetQueryable(AppendWhere<E>(Queryable, FKFieldName, id), operation);
        }


        private static IQueryable<I> AppendWhere<I>(IQueryable<I> Queryable, string FieldName, string Value)
        {
            var type = typeof(I);

            var keyField = (PropertyInfo)type.GetMember(FieldName)[0];

            var key = int.Parse(Value);

            ParameterExpression prm = Expression.Parameter(typeof(I), "x");
            MemberExpression member = Expression.MakeMemberAccess(prm, keyField);
            ConstantExpression idParam = Expression.Constant(key, keyField.PropertyType);
            BinaryExpression expr = Expression.Equal(member, idParam);
            Expression<Func<I, bool>> lambda = Expression.Lambda<Func<I, bool>>(expr, new ParameterExpression[] { prm });

            return Queryable.Where(lambda);
        }

        private static IQueryable<T> GetQueryable(IQueryable<E> Query, ODataQueryOperation operation)
        {
            var q = LinqMapper.Mapper.Map<E, T>(Query, GetExpands()); // Query.Project().To<T>(GetExpands());

            if (operation.FilterExpression != null)
            {
                var expr = (System.Linq.Expressions.Expression<System.Func<T, bool>>)((UnaryExpression)operation.FilterExpression).Operand;

                q = q.Where(expr);
            }

            if (operation.TopCount > 0)
            {
                q = q.Take(operation.TopCount);
            }

            if (operation.SkipCount > 0)
            {
                q = q.Take(operation.SkipCount);
            }

            return q;
        }
         */
    }
}
