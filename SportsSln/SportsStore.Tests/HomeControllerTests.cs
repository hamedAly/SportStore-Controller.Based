using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace SportsStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Can_Paginate()
    {
        // arange 
        Mock<IStoreRepository<Product>> repoMock = new Mock<IStoreRepository<Product>>();
        repoMock.Setup(m => m.Products).Returns((new Product[] {
                                                new Product {ProductID = 1, Name = "P1"},
                                                new Product {ProductID = 2, Name = "P2"},
                                                new Product {ProductID = 3, Name = "P3"},
                                                new Product {ProductID = 4, Name = "P4"},
                                                new Product {ProductID = 5, Name = "P5"}
                                                }).AsQueryable<Product>());

        HomeController homeControler = new HomeController(repoMock.Object);
        homeControler.pageSize = 3;
        IEnumerable<Product> result =(homeControler.Index(null,2) as ViewResult)?.ViewData.Model as IEnumerable<Product> ?? Enumerable.Empty<Product>();

        Product[] prodArray = result.ToArray();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }
    [Fact]
    public void Can_Use_Repository()
    {
        // Arrange
        Mock<IStoreRepository<Product>> mock = new Mock<IStoreRepository<Product>>();
        mock.Setup(m => m.Products).Returns((new Product[] {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"}
                    }).AsQueryable<Product>());
        HomeController controller = new HomeController(mock.Object);
        // Act
        ProductsListViewModel result = (controller.Index(null) as ViewResult)?.ViewData.Model as ProductsListViewModel ?? new();
        // Assert
        Product[] prodArray = result.Products.ToArray();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);
    }
}