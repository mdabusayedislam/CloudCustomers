using CloudCusromers.API.Config;
using CloudCusromers.API.Models;
using CloudCusromers.API.Services;
using CloudCustomers.UnitTexts.Fixtures;
using CloudCustomers.UnitTexts.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTexts.Systems.Services
{
    public class TestUserService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrage
            var expectedResponse = UserFixture.GetTestUsers();

            var handlerMock = MockHttpMessageHandler<User>
                .SetUpBasicGetResourceList(expectedResponse);

            var httpClient=new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req=>req.Method==HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsEmptyListOfUsers()
        {
            //Arrage
            var expectedResponse = UserFixture.GetTestUsers();

            var handlerMock = MockHttpMessageHandler<User>
                .SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            var result=await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrage
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetUpBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokedConfigureExternalUrl()
        {
            //Arrage
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetUpBasicGetResourceList(expectedResponse,endpoint);
            var httpClient = new HttpClient(handlerMock.Object);
           
            var config = Options.Create(
                   new UserApiOptions{
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient,config);

            //Act
            var result = await sut.GetAllUsers();

            var uri=new Uri(endpoint);

            //Assert
            handlerMock
                 .Protected()
                 .Verify(
                 "SendAsync",
                 Times.Exactly(1),
                 ItExpr.Is<HttpRequestMessage>(
                     req => req.Method == HttpMethod.Get
                     && req.RequestUri==uri),
                 ItExpr.IsAny<CancellationToken>()
                 );
        }

    }
}
