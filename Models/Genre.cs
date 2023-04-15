using System.ComponentModel.DataAnnotations;

namespace filmsystemet.Models
{
	public class Genre
	{
		public int Id { get; set; }
		[StringLength(50)]
        public string GenreName { get; set; }

		[StringLength(200)]
		public string Description { get; set; }

	}
}
