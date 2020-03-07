using System;
using System.Collections.Generic;

namespace API_Project.Models
{
    public partial class FavoriteMovies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? MovieYear { get; set; }
        public string UserId { get; set; }
        public string MovieId { get; set; }
        public int UserRating { get; set; }

        public virtual AspNetUsers User { get; set; }

        public FavoriteMovies(){ }

        public FavoriteMovies(string _title, int _movieYear, string _movieId, int _userRating)
        {
            Title = _title;
            MovieYear = _movieYear;
            MovieId = _movieId;
            UserRating = _userRating;
        }
    }
}
