using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataClientServices: IUserDataClientServices
    {
        private readonly HttpClient _httpClient;

        public UserDataClientServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserDataInsertDTO>> DownloadUserData()
        {
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await result.Content.ReadAsStringAsync();
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var userDataResponse = JsonSerializer.Deserialize<IEnumerable<UserDataInsertDTO>>(body, jsonOptions);

            return userDataResponse;
        }
    }
}
