using NewsDA.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsDA.UnitOfWork
{
   public interface IUnitOfWork:IDisposable
    {
       INewsDal _newsDal { get; }
       ICategoryDal _categoryDal { get; }
       void Save();
    }
}
