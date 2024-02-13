using FullStack.ServiceModel;
using FullStack.ServiceModel.DTO;
using FuulStack_Test.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.xUnitTest
{
    
    public class MovieServiceTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MovieServiceTest(TestingWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            _configuration = factory.Services.GetService<IConfiguration>();
        }



        public static IEnumerable<object[]> SearchData()
        {
            yield return new object[] { "Avengers", "", 1 };
            yield return new object[] { "", "tt2036450", 1 };
            yield return new object[] { "Red", "", 1 };
        }

        public static IEnumerable<object[]> SearchDataTwo()
        {
            yield return new object[] { "Avengers", "", 1 };
        }

        public static IEnumerable<object[]> SearchDataThree()
        {
            yield return new object[] { "", "tt2036450", 1 };
        }

        [Theory]
        [MemberData(nameof(SearchData))]
        public async void MovieSearch_NOTNULL(string title, string id, int PageNum)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Movie/MovieSearch?title={title}&id={id}&PageNum={PageNum}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.True(!string.IsNullOrEmpty(result));
        }

        [Theory]
        [MemberData(nameof(SearchDataTwo))]
        public async void MovieSearch_ReturnsEntity(string title, string id, int PageNum)
        {
            var expectedResult = new List<MovieSearch>();
            expectedResult.Add(new MovieSearch
            {
                Title = "The Avengers",
                imdbID = "tt0848228"
            });
            expectedResult.Add(new MovieSearch
            {
                Title = "Avengers: Endgame",
                imdbID = "tt0090190+"
            });
            var response = await _httpClient.GetAsync($"/api/v1/Movie/MovieSearch?title={title}&id={id}&PageNum={PageNum}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.Contains(expectedResult[0].Title, result);
            Assert.Contains(expectedResult[0].imdbID, result);
            Assert.Contains(expectedResult[1].Title, result);
        }
                        
    }
}
