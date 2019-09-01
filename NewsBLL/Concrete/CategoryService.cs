using NewsBLL.Abstract;
using NewsDA.Abstract;
using NewsDA.UnitOfWork;
using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBLL.Concrete
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _UoW;
        public CategoryService(IUnitOfWork UoW )
        {
            this._UoW = UoW;
        }
        public void Add(Category category)
        {
            _UoW._categoryDal.Add(category);
            _UoW.Save();
        }

        public void Delete(Category category)
        {
            _UoW._categoryDal.Delete(category);
            _UoW.Save();
        }

        public List<Category> GetAll()
        {
           return _UoW._categoryDal.GetAll();
        }

        public void Update(Category category)
        {
            _UoW._categoryDal.Update(category);
            _UoW.Save();
        }
    }
}
