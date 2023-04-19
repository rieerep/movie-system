using System;
using System.Collections.Generic;

namespace filmsystemet.Models
{
    public partial class Person
    {
        public Person()
        {
            FavouriteGenres = new HashSet<FavouriteGenre>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<FavouriteGenre> FavouriteGenres { get; set; }
    }
}
