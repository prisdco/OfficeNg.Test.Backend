using FullStack.ServiceModel;
using FuulStack_Test.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace FullStack.xUnitTest
{
    [Collection("ApplicationClientFactory")]
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;
        public UnitTest1(WebApplicationFactory<Program> webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();

        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { "Avengers", "", 1 };
            yield return new object[] { "", "tt2036450", 1 };
            yield return new object[] { "Red", "", 1 };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async void MovieSearch(string title, string id, int PageNum)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Movie/MovieSearch?title={title}&id={id}&PageNum={PageNum}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.True(!string.IsNullOrEmpty(result));
        }

    }
}