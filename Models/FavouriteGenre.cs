using System;
using System.Collections.Generic;

namespace filmsystemet.Models
{
    public partial class FavouriteGenre
    {
        public int Id { get; set; }
        public string Movies { get; set; }
        public decimal? Rating { get; set; }
        public int PersonId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Person Person { get; set; }
    }
}
