using FullStack.ServiceModel.DTO;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.ServiceModel
{
    public interface IApplicationClientFactory
    {
        [Get("/api/GetMovies")]
        Task<SearchM> GetMovies(string sParam = "", int pageNumber = 1);

        [Get("/api/GetMovie")]
        Task<MovieList> GetMovie(string iParam = "");
    }
}
