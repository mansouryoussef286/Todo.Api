using Newtonsoft.Json;
using System.Net.Http;
using Todo.Api.Domain.Models;

namespace Todo.Api.Domain.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AuthenticationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<AuthenticationResModel> Authenticate(string code)
        {
            // Replace the URL with the actual API endpoint
            var apiUrl = (_configuration["Auth-Server:ApiUrl"] ?? "") + "authenticate";
            var authServerApiId = _configuration["Auth-Server:ApiId"] ?? "";
            var authServerApiSecret = _configuration["Auth-Server:ApiISecret"] ?? "";

            var reqModel = new AuthenticationReqModel()
            {
                Code = code,
                ApiId = authServerApiId,
                ApiSecret = authServerApiSecret
            };


            var jsonContent = JsonConvert.SerializeObject(reqModel);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            response.EnsureSuccessStatusCode();

            // Read and deserialize the response content
            var responseContent = await response.Content.ReadAsStringAsync();
            var authenticationResModel = JsonConvert.DeserializeObject<AuthenticationResModel>(responseContent);
           
            if (authenticationResModel != null && authenticationResModel.Success == true)
            {
                return authenticationResModel;

            }
            else
            {
                authenticationResModel = new AuthenticationResModel();
                authenticationResModel.Success = false;
                return authenticationResModel;
            }
        }
    }
}
