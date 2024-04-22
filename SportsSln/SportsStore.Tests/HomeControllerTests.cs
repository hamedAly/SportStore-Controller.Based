using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
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
        IEnumerable<Product> result =
           (homeControler.Index(2) as ViewResult)?
           .ViewData.Model as IEnumerable<Product> 
           ?? Enumerable.Empty<Product>();

        Product[] prodArray = result.ToArray();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }
}