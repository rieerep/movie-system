using filmsystemet.Data;
using filmsystemet.Models;
using filmsystemet.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace filmsystemet
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
			{
				builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
			}));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseCors("corsapp");
			}

			// app.UseHttpsRedirection();

			// app.UseAuthorization();


			 // GET Hämta alla personer i systemet
			app.MapGet("/persons", (HttpContext httpContext) =>
			{
				PersonRepository personRepo = new PersonRepository(new MovieSystemDbContext());

				return personRepo.GetAll();
			}).WithName("GetPersons");


			// Get genres
			app.MapGet("/genres", (HttpContext httpContext) =>
			{
				GenreRepository genreRepo = new GenreRepository(new MovieSystemDbContext());

				return genreRepo.GetAll();
			}).WithName("GetGenres");


			// GET Hämtar genrer kopplade till person i systemet
			app.MapGet("/persongenres/{personId}", (int personId, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				PersonRepository personRepo = new PersonRepository (movieSystemDbContext);
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);
				GenreRepository genreRepo = new GenreRepository(movieSystemDbContext);
				var genres = favGenRepo.GetByCondition(fg => fg.PersonId == personId & fg.Movies == null).Join(genreRepo.GetAll(),
					favGen => favGen.GenreId,
					genre => genre.Id,
					(favG, gen) => new { Genre = gen }).ToList();


				//var studentToStandard = studentRep.GetAll().Join(standardRep.GetAll(),
				//		student => student.StandardRefId,
				//		standard => standard.StandardId,
				//		(stud, stand) => new { Student = stud, Standard = stand }).ToList();

				return genres;
			}).WithName("GetPersonGenre");


			// GET Hämtar alla filmer kopplade till en viss person
			app.MapGet("/personmovies/{personId}", (int personId, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				PersonRepository personRepo = new PersonRepository(movieSystemDbContext);
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);
				GenreRepository genreRepo = new GenreRepository(movieSystemDbContext);
				var movies = favGenRepo.GetByCondition(m => m.PersonId == personId & m.Movies != null).ToList();

				return movies;
			}).WithName("GetPersonMovie");

			// GET hämta "rating" på filmer kopplat till en person
			app.MapGet("/movierating/{personId}", (int personId, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);
				var personRating = favGenRepo.GetByCondition(r => r.PersonId == personId & r.Rating != null).ToList();


				//var studentToStandard = studentRep.GetAll().Join(standardRep.GetAll(),
				//		student => student.StandardRefId,
				//		standard => standard.StandardId,
				//		(stud, stand) => new { Student = stud, Standard = stand }).ToList();

				return personRating;
			}).WithName("GetRating");


			// PUT Lägg till "rating" på filmer kopplat till en person
			app.MapPost("/setrating", (FavouriteGenre addRating, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);
				
				// var addRating = favGenRepo.Update(addRating).ToList();

				favGenRepo.Create(addRating);
				movieSystemDbContext.SaveChanges();

				return addRating;
			}).WithName("AddRating");

			// POST Koppla en person till en ny genre
			app.MapPost("/addgenre", (FavouriteGenre addGenre, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);

				favGenRepo.Create(addGenre);
				movieSystemDbContext.SaveChanges();
				return addGenre;
			}).WithName("AddGenre");

			// POST Lägg in nya länkar/film för en specifik person
			app.MapPost("/addlink", (FavouriteGenre addGenre, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);

				favGenRepo.Create(addGenre);
				movieSystemDbContext.SaveChanges();
				return addGenre;
			}).WithName("AddLink");

			// POST Lägg in nya länkar för en specifik genre
            app.MapPost("/addmovietogenre/{genre}/{movie}", (FavouriteGenre addmovietogenre, HttpContext httpContext) =>
            {
                MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
                FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);

                favGenRepo.Create(addmovietogenre);
                movieSystemDbContext.SaveChanges();
                return addmovietogenre;
            }).WithName("AddMovie");

			// GET Få förslag på filmer i en viss genre från ett externt API
			app.MapGet("/moviesuggestion/{tmdb_id}", async (int tmdb_id, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);
                GenreRepository genreRepo = new GenreRepository(movieSystemDbContext);
                var tmdbUrl = $"https://api.themoviedb.org/3/discover/movie?api_key=cb362116c7c70793fce05c7369dc033c&with_genres={tmdb_id}";

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(tmdbUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
			}).WithName("GetRecommended");

			app.Run();
		}
	}
}