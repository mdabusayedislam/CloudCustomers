using CloudCusromers.API.Config;
using CloudCusromers.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCusromers.API.Services
{
    public class UserService:IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UserApiOptions _apiConfig;
        public UserService(
            HttpClient httpClient,
            IOptions<UserApiOptions> apiConfig )
        {
           _httpClient= httpClient;
           _apiConfig = apiConfig.Value;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if(userResponse.StatusCode==System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }     
            var responseContent=userResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
