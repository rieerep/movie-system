namespace filmsystemet.Models
{
	public class FavouriteGenre
	{
        public int Id { get; set; }
        public string Movies { get; set; }
        public double Rating { get; set; }
        public string Link { get; set; }
		public ICollection<Person> PersonId { get; set; }
        public ICollection<Genre> GenreId { get; set; }
    }
}
