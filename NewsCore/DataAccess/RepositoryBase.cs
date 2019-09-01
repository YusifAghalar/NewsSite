using Microsoft.EntityFrameworkCore;
using NewsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NewsCore.DataAccess
{
    public abstract class RepositoryBase<T,Tcontext> : IRepositoryBase<T> 
        where T : class, IEntity, new()
        where Tcontext:DbContext,new()
    {
        
        public void Add(T item)
        {
            using(Tcontext tcontext=new Tcontext())
            {
                var addedEntity = tcontext.Entry(item);
                addedEntity.State = EntityState.Added;
                tcontext.SaveChanges();
            }
        }

        public void Delete(T item)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                var deletedEntity = tcontext.Entry(item);
                deletedEntity.State = EntityState.Deleted;
                tcontext.SaveChanges();
            }
        }

        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            using(Tcontext tcontext = new Tcontext())
            {
                return tcontext.Set<T>().SingleOrDefault(filter);
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                return filter == null
                                    ? tcontext.Set<T>().ToList() 
                                    : tcontext.Set<T>().Where(filter).ToList();
            }

        }

        public void Update(T item)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                var updatedEntity= tcontext.Entry(item);
                updatedEntity.State = EntityState.Modified;

                tcontext.SaveChanges();
            }
        }
    }
}
