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

			app.MapGet("/persons", (HttpContext httpContext) =>
			{
				PersonRepository personRepo = new PersonRepository(new MovieSystemDbContext());

				return personRepo.GetAll();
			}).WithName("GetPersons");

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

			app.MapGet("/personmovies/{personId}", (int personId, HttpContext httpContext) =>
			{
				MovieSystemDbContext movieSystemDbContext = new MovieSystemDbContext();
				PersonRepository personRepo = new PersonRepository(movieSystemDbContext);
				FavouriteGenreRepository favGenRepo = new FavouriteGenreRepository(movieSystemDbContext);
				GenreRepository genreRepo = new GenreRepository(movieSystemDbContext);
				var movies = favGenRepo.GetByCondition(m => m.PersonId == personId & m.Movies != null).ToList();


				//var studentToStandard = studentRep.GetAll().Join(standardRep.GetAll(),
				//		student => student.StandardRefId,
				//		standard => standard.StandardId,
				//		(stud, stand) => new { Student = stud, Standard = stand }).ToList();

				return movies;
			}).WithName("GetPersonMovie");

			app.MapPost("/")

			var summaries = new[]
			{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

			app.MapGet("/weatherforecast1", (HttpContext httpContext) =>
			{
				var forecast = Enumerable.Range(1, 5).Select(index =>
					new WeatherForecast
					{
						Date = DateTime.Now.AddDays(index),
						TemperatureC = Random.Shared.Next(-20, 55),
						Summary = summaries[Random.Shared.Next(summaries.Length)]
					})
					.ToArray();
				return forecast;
			})
			.WithName("GetWeatherForecast1");

			app.Run();
		}
	}
}