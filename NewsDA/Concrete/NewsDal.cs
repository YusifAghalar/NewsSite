using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NewsCore.DataAccess;
using NewsDA.Abstract;
using NewsDA.Context;
using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NewsDA.Concrete
{
    public class NewsDal : RepositoryBase<News, ApplicationDbContext>, INewsDal
    {
        private ApplicationDbContext db;
        public NewsDal(ApplicationDbContext _db)
        {
            db = _db;
        }

        public List<News> GetHeaderNews(int count)
        {

            return db.News.Where(x => x.IsHeaderNews).OrderByDescending(x => x.CreatedOn)
                                                     .Take(count).ToList();

        }

        public IQueryable<News> GetAllWithCategories(Expression<Func<News, bool>> filter = null)
        {

            return filter == null
                                 ? db.Set<News>().Include(x => x.Category)
                                 : db.Set<News>().Include(x => x.Category).Where(filter);



        }
        public override News Get(Expression<Func<News, bool>> filter)
        {
            return db.Set<News>().Include(X => X.Category).FirstOrDefault(filter);

        }
        public int Count()
        {
            return db.Set<News>().Count();
        }
        public List<News> GetByAuthorId(string authorid)
        {
            return db.Set<News>().Include(x => x.Category).Where(x => x.AuthorId == authorid).ToList();

        }

        public List<News> SearchByCategoryAndSearchterm(string searchTerm, string categoryName)
        {
            var result = db.Set<News>().Include(x => x.Category)
                .Where(x =>
                      (/*String.IsNullOrEmpty(categoryName) ||*/x.Category.Name == categoryName) &&
                      (/*String.IsNullOrEmpty(searchTerm) ||*/ x.Title.Contains(searchTerm))
                      )
                .AsQueryable();

            //if (!String.IsNullOrEmpty(searchTerm))
            //    result = result.Where(x => x.Title.Contains(searchTerm));

            //if (!String.IsNullOrEmpty(categoryName))
            //    result = result.Where(x => x.Category.Name == categoryName);

            return result.ToList();

        }


    }
}
