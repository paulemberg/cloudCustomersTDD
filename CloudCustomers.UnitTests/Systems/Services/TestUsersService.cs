using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokeHttpGetRequest()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://exemple.com.br";
        var config = Options.Create(new UserApiOptions
        {
            EndPoint = endpoint
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
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
       
        var endpoint = "https://exemple.com.br";
        var config = Options.Create(new UserApiOptions
        {
            EndPoint = endpoint
        }); 
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Should().BeOfType<List<User>>();
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsEmptyListOfUsers()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://exemple.com.br";
        var config = Options.Create(new UserApiOptions
        {
            EndPoint = endpoint
        }); 
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Count.Should().Be(0);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://exemple.com.br";
        var config = Options.Create(new UserApiOptions
        {
            EndPoint = endpoint
        }); 
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Count.Should().Be(expectedResponse.Count);
    }
    
        
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var endpoint = "https://exemple.com.br";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options.Create(new UserApiOptions
        {
            EndPoint = endpoint
        }); 
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();
        var uri = new Uri(endpoint);
        //Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get 
                           && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
            );
    }
    
}