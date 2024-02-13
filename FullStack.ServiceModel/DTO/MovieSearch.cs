using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.ServiceModel.DTO
{
    public class SearchM
    {
        public List<MovieSearch> Search { get; set; }
        public string totalResults { get; set; }
    }
    public class MovieSearch
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
