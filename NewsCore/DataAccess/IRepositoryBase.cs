using NewsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NewsCore.DataAccess
{
    public interface IRepositoryBase<T> where T:class,IEntity,new()
    {
        T Get(Expression<Func<T,bool>>  filter);

        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        void Add(T item);
        void Update(T item);
        void Delete(T item);

    }
}