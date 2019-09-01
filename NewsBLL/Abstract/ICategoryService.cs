using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBLL.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        void Delete(Category category);
        void Add(Category category);
        void Update(Category category);
    }
}
