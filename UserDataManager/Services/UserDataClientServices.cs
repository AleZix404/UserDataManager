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
        private HttpClient _httpClient;
        private IMapper _mapper; 

        public UserDataClientServices(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDataInsertDTO>> SetUserDataClient()
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
