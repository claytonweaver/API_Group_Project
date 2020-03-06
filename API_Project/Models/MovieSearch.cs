using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Project.Models
{

    public class MovieSearch
    {
        public MovieOverview[] Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

    public class MovieOverview
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public string UserId { get; set; }
    }

}
