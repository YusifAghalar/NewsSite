using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NewsBLL.Abstract
{
    public interface INewsService
    {
        News Get(int id);
        List<News> GetAll();
        List<News> GetByCategory(int categoryId);
        List<News> GetHeaderNews(int count);
        IQueryable<News> GetAllWithCategories(Expression<Func<News, bool>> expression=null);
        List<News> SearchByCategoryandSearchTerm(string searchTerm, string categoryName);
        void Delete(News news);
        void Add(News news);
        void Update(News news);
        int Count();
    }
}
