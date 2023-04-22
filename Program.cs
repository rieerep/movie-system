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

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// app.UseHttpsRedirection();

			// app.UseAuthorization();


			 // GET Hämta alla personer i systemet
			app.MapGet("/persons", (HttpContext httpContext) =>
			{
				PersonRepository personRepo = new PersonRepository(new MovieSystemDbContext());

				return personRepo.GetAll();
			}).WithName("GetPersons");


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
			app.MapPost("/setrating/{Id}/{rating}", (int personId, FavouriteGenre addRating, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);
				
				// var addRating = favGenRepo.Update(addRating).ToList();

				favGenRepo.Update(addRating);
				movieSystemDbContext.SaveChanges();

				return addRating;
			}).WithName("AddRating");

			// POST Koppla en person till en ny genre

			app.MapPost("/addgenre/{personId}/{genre}", (int personId, FavouriteGenre addGenre, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);

				favGenRepo.Create(addGenre);
				movieSystemDbContext.SaveChanges();
				return addGenre;
			}).WithName("AddGenre");




			app.Run();
		}
	}
}