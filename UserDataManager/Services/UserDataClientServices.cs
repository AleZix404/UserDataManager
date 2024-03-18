using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataClientServices: IUserDataClientServices
    {
        private HttpClient _httpClient;

        public UserDataClientServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserData.UserDataResponse>> SetUserDataClient()
        {
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await result.Content.ReadAsStringAsync();
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var userDataResponse = JsonSerializer.Deserialize<IEnumerable<UserData.UserDataResponse>>(body, jsonOptions);

            return userDataResponse;
        }
    }
}
