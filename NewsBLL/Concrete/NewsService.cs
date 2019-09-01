using NewsBLL.Abstract;
using NewsDA.Concrete;
using NewsDA.Abstract;
using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using NewsDA.UnitOfWork;

namespace NewsBLL.Concrete
{
    public class NewsService : INewsService
    {
        private IUnitOfWork _UoW;
        public NewsService(IUnitOfWork UoW)
        {
            this._UoW = UoW;
        }
        public void Add(News news)
        {
            _UoW._newsDal.Add(news);
            _UoW.Save();
        }

        public void Delete(News news)
        {
            _UoW._newsDal.Delete(news);
            _UoW.Save();
        }

       
        public News Get(int id )
        {
            return _UoW._newsDal.Get(x => x.Id==id);
        }

        public List<News> GetAll()
        {
            return _UoW._newsDal.GetAll();
        }

        public List<News> GetByCategory(int categoryId)
        {
            return _UoW._newsDal.GetAll(x => x.CategoryId == categoryId);
        }

        public List<News> GetHeaderNews(int count)
        {
            return _UoW._newsDal.GetHeaderNews(count);
        }

        public IQueryable<News> GetAllWithCategories(Expression<Func<News, bool>> filter = null)
        {
            return _UoW._newsDal.GetAllWithCategories(filter);
        }
        public List<News> SearchByCategoryandSearchTerm(string searchTerm, string categoryName)
        {
            return _UoW._newsDal.SearchByCategoryAndSearchterm(searchTerm, categoryName);
        }

        public void Update(News news)
        {
            _UoW._newsDal.Update(news);
           
        }
        public int Count()
        {
            return _UoW._newsDal.Count();
        }
    }
}
