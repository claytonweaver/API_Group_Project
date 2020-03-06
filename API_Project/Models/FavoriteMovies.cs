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

        public virtual AspNetUsers User { get; set; }
    }
}
