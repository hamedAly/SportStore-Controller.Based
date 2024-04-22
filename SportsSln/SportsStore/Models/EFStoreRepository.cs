
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository<Product>
    {
        StoreDbContext _dbContext;
        public EFStoreRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Product> Products => _dbContext.Products;
    }
}
