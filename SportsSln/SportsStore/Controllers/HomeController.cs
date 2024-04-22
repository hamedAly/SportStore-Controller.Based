using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository<Product> repo;
        public int pageSize = 4;
        public HomeController(IStoreRepository<Product> _repo)
        {
            repo = _repo;
        }
        public IActionResult Index(int productPage = 1) =>
            View(
                new ProductsListViewModel
                {
                    Products = repo.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * pageSize)
                    .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = pageSize,
                        TotalItems = repo.Products.Count()
                    }
                });
    }
}
