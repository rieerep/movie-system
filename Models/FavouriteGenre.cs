namespace filmsystemet.Models
{
	public class FavouriteGenre
	{
        public int Id { get; set; }
        public string Movies { get; set; }
        public double Rating { get; set; }
        public Person PersonId { get; set; }
        public Genre GenreId { get; set; }
    }
}
