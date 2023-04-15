using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace filmsystemet.Models
{
    public class Person
    {
        public int Id { get; set; }

		[StringLength(15)]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[StringLength(20)]
		[DisplayName("Last Name")]
		public string LastName { get; set; } = null!;
        

	}
}
