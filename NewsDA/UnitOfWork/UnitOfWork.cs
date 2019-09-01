using NewsDA.Abstract;
using NewsDA.Concrete;
using NewsDA.Context;
using System;


namespace NewsDA.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ApplicationDbContext dbContext;
        public INewsDal _newsDal { get;  private set; }
        public ICategoryDal _categoryDal { get; private set; }
        public UnitOfWork(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
            _newsDal = new NewsDal(dbContext);
            _categoryDal = new CategoryDal(dbContext);
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();

                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
      
    }
}

