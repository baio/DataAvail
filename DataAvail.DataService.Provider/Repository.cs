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
using DataAvail.LinqMapper;

namespace DataAvail.DataService.Provider
{
    public abstract class Repository<E, T> : IRepository
        where E : class
        where T : class, new()
    {
        public Repository()
        {
        }

        protected ObjectContext Context { get; private set; }

        public void SetContext(ObjectContext Context)
        {
            this.Context = Context;
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
            var q = Mapper.Map<E, T>(Queryable, GetExpands());

            q = FilterByKey(q, id);

            return q.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(ODataQueryOperation operation)
        {
            var q = Mapper.Map<E, T>(Queryable, GetExpands());

            q = OnGetQuery(q);

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

        protected virtual IQueryable<T> OnGetQuery(IQueryable<T> Query)
        {
            return Query;
        }

        private string KeyFieldName
        {
            get
            {
                var type = typeof(T);

                return type.GetCustomAttributes(false).OfType<DataServiceKeyAttribute>().SingleOrDefault().KeyNames[0];                
            }
        }

        private int GetKeyFieldValue(T Item)
        {
            return (int)Item.GetType().GetProperty(KeyFieldName).GetValue(Item, null);
        }

        private void SetKeyFieldValue(T Item, object Val)
        {
            Item.GetType().GetProperty(KeyFieldName).SetValue(Item, Val, null);
        }


        private IQueryable<T> FilterByKey(IQueryable<T> Queryable, string KeyValue)
        {
            return FilterBy(Queryable, KeyFieldName, KeyValue);
        }

        private IQueryable<T> FilterBy(IQueryable<T> Queryable, string FieldName, string Value)
        {
            var type = typeof(T);

            var keyField = (PropertyInfo)type.GetMember(FieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)[0];

            var key = int.Parse(Value);

            ParameterExpression prm = Expression.Parameter(typeof(T), "x");
            MemberExpression member = Expression.MakeMemberAccess(prm, keyField);
            ConstantExpression idParam = Expression.Constant(key, keyField.PropertyType);
            BinaryExpression expr = Expression.Equal(member, idParam);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(expr, new ParameterExpression[] { prm });

            return Queryable.Where(lambda);
        }


        public object CreateDefaultEntity()
        {
            var item = new T();

            SetKeyFieldValue(item, -1);

            return item;

        }

        public void Save(object Item)
        {
            if (Item.GetType().IsArray)
                Item = ((T[])Item)[0];

            var keyVal = GetKeyFieldValue((T)Item);

            var entity = AutoMapper.Mapper.Map<T, E>((T)Item);

            if (keyVal < 0)
            {
                Context.AddObject(typeof(E).Name + "s", entity);

                Context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Added);
            }
            else
            {

                Context.AttachTo(typeof(E).Name + "s", entity);

                Context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
            }

            Context.SaveChanges();

            AutoMapper.Mapper.Map<E, T>(entity, (T)Item);
        }

        public void Remove(object Item)
        {
            var entity = AutoMapper.Mapper.Map<T, E>((T)Item);

            Context.AttachTo(typeof(E).Name + "s", entity);

            Context.DeleteObject(entity);

            Context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Deleted);

            Context.SaveChanges();
        }

        public IQueryable<T> GetAllByForeignKey(string FKFieldName, string FKValue)
        { 
            return GetAllByForeignKey(null, FKFieldName, FKValue);
        }

        public IQueryable<T> GetAllByForeignKey(ODataQueryOperation operation, string FKFieldName, string FKValue)
        {
            var q = Mapper.Map<E, T>(Queryable, GetExpands());

            q = FilterBy(q, FKFieldName, FKValue);

            if (operation != null)
            {
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
            }

            return q;
        }



    }
}
