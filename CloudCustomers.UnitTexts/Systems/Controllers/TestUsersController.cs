using CloudCusromers.API.Controllers;
using CloudCusromers.API.Models;
using CloudCusromers.API.Services;
using CloudCustomers.UnitTexts.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace CloudCustomers.UnitTexts.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arrage
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(
            service => service.GetAllUsers())
            .ReturnsAsync(UserFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);

        //Act
        var result =(OkObjectResult) await sut.Get();

        //Assert
        result.StatusCode.Should().Be(200);

    }
    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyOnce()
    {
        //Arrage
        var mockUserService =new Mock<IUserService>();
        mockUserService.Setup(
            service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = await sut.Get();

        //Assert
        mockUserService.Verify(
            service=>service.GetAllUsers(),
            Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        //Arrage
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service =>
        service.GetAllUsers())
        .ReturnsAsync(UserFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);
        //Act
        var result = await sut.Get();

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUserFound_Returns404()
    {
        //Arrage
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service =>
        service.GetAllUsers())
        .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);
        //Act
        var result = await sut.Get();

        //Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult=(NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
        
    }


}
