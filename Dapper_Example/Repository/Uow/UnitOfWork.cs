using DapperExample.Repository.Interfaces;

namespace DapperExample.Repository.Uow
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext?.Transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _dbContext?.Transaction?.Rollback();
            Dispose();
        }

        public void Dispose() => _dbContext.Transaction?.Dispose();
    }
}
