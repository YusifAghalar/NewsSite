using NewsCore.DataAccess;
using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsDA.Abstract
{
    public interface ICategoryDal:IRepositoryBase<Category>
    {
        /// category custom kodu
    }
}
