using FullStack.ServiceModel;
using FullStack.ServiceModel.DTO;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace FuulStack_Test.Service
{
    public class ApplicationClientFactory : IApplicationClientFactory
    {
        private HttpClient Client { get; }
        private readonly JsonSerializerOptions _options;

        public ApplicationClientFactory(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("BASE_ADDRESS"));
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Client = client;
        }


        public async Task<MovieList> GetMovie(string iParam = "")
        {
            try
            {
                string UseParam = $"i={iParam}";

                var request = new HttpRequestMessage(HttpMethod.Get, "?apikey=dcc577a3&" + UseParam);
                using var response = await Client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<MovieList>(content, _options);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<SearchM> GetMovies(string sParam = "", int pageNumber = 1)
        {
            try
            {
                string UseParam = $"s='{sParam}'&page={pageNumber}";

                var request = new HttpRequestMessage(HttpMethod.Get, "?apikey=dcc577a3&" + UseParam);
                using var response = await Client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<SearchM>(content, _options);
            }
            catch(Exception ex)
            {
                return null;
            }
        }



    }
}
