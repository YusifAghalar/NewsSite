using NewsEntities.Concrete;
using NewsCore.DataAccess;
using NewsDA.Abstract;
using NewsDA.Context;

namespace NewsDA.Concrete
{
    public class CategoryDal:RepositoryBase<Category,ApplicationDbContext>,ICategoryDal
    {
        private ApplicationDbContext _dbContext;
        public CategoryDal(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
    }
}
