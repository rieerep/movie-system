# Movie-system
Creating my own REST API. Using Entity Framework Core, C#, SSMS, SQL.
This is a project where I tried to use repository pattern.

This product uses the TMDb API but is not endorsed or certified by TMDb (https://www.themoviedb.org/).

![enter image description here](https://www.themoviedb.org/assets/2/v4/logos/v2/blue_long_2-9665a76b1ae401a510ec1e0ca40ddcb3b0cfe45f1d51b77a308fea0845885648.svg)

## Languages used

 - C#
 - SQL
## API Calls
 - /persons - GET Hämta alla personer i systemet - Gets all users in the system
 - /persongenres/{personId} - GET Gets all favourite genres connected to a certain user (example persongenres/3)
 - /personmovies/{personId} - GET Gets all movies connected to a certain user (example personmovies/1)
 - movierating/{personId}  - GET Gets all ratings on movies made by certain user (example movierating/1)
 - /addgenre/{personId}/{genre} - POST Connect a user to a new genre
 - /addlink/{personId}/{genre} - POST Connect a user to a new movie
 - /addmovietogenre/{genre}/{movie}  - POST Connect a new movie to a new Genre.

