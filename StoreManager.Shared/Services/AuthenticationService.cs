using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AKSoftware.WebApi.Client;
using StoreManager.Shared.Models;

namespace StoreManager.Shared.Services
{
    public class AuthenticationService
    {

        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();

        public AuthenticationService(string url)
        {
            _baseUrl = url;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterRequest request)
        {
            var response = await client.PostAsync<UserManagerResponse>($"{_baseUrl}/api/identity/register", request);
            return response.Result;
        }
        public async Task<UserManagerResponse> LoginUserAsync(LoginRequest request)
        {
            var response = await client.PostAsync<UserManagerResponse>($"{_baseUrl}/api/identity/token", request);
            return response.Result;
        }
        

    }
}
