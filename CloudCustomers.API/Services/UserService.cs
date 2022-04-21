using System.Net;
using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomers.API.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly UserApiOptions _apiConfig;

    public UserService(HttpClient httpClient, IOptions<UserApiOptions> apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var userResponse = await _httpClient.GetAsync(_apiConfig.EndPoint);
        if(userResponse.StatusCode == HttpStatusCode.NotFound)
            return new List<User> { };

        var responseContent = userResponse.Content;
        var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
        return allUsers.ToList();

    }
}