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
        public IActionResult Index(string? category, int productPage = 1) =>
            View(
                new ProductsListViewModel
                {
                    Products = repo.Products
                    //.Where(p =>p.Category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * pageSize)
                    .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = pageSize,
                        TotalItems = repo.Products.Where(p => p.Category == null || p.Category == category).Count()
                    },
                    CurrentCategory = category?.ToString()
                });
    }
}
