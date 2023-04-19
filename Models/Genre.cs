using System;
using System.Collections.Generic;

namespace filmsystemet.Models
{
    public partial class Genre
    {
        public Genre()
        {
            FavouriteGenres = new HashSet<FavouriteGenre>();
        }

        public int Id { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FavouriteGenre> FavouriteGenres { get; set; }
    }
}
