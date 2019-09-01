using Microsoft.EntityFrameworkCore.Query;
using NewsCore.DataAccess;
using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NewsDA.Abstract
{
   public interface INewsDal:IRepositoryBase<News>
    {
        List<News> GetHeaderNews(int count);
        IQueryable<News> GetAllWithCategories(Expression<Func<News, bool>> filter);
        List<News> SearchByCategoryAndSearchterm(string searchTerm, string categoryName);
        int Count();
    }
}
