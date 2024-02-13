using api_cinema_challenge.DTO;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.Endpoints
{
    public static class MovieEndpoint
    {
        public static void ConfigureMovieEndpoint(this WebApplication app)
        {
            var cinema = app.MapGroup("/movies");
            cinema.MapGet("", GetAllMovies);
            cinema.MapPost("", CreateMovie);
            cinema.MapPut("/{id}", UpdateMovie);
            cinema.MapDelete("/{id}", DeleteMovie);
        }

        public static async Task<IResult> GetAllMovies(ICinemaRepository repository)
        {
            var movies = await repository.GetAllMovies();
            return TypedResults.Ok(new MovieListResponseDTO("success", movies));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Manager")]
        public static async Task<IResult> CreateMovie(CreateMoviePayload payload, ICinemaRepository repository)
        {
            if (string.IsNullOrEmpty(payload.Title))
            {
                return TypedResults.BadRequest("Title is required");
            }
            if (string.IsNullOrEmpty(payload.Rating))
            {
                return TypedResults.BadRequest("Rating is required");
            }
            if (string.IsNullOrEmpty(payload.Description))
            {
                return TypedResults.BadRequest("Description is required");
            }
            if (payload.RunTimeMinutes <= 0)
            {
                return TypedResults.BadRequest("RunTimeMinutes must be greater than 0");
            }
            var movie = await repository.CreateMovie(payload);
            return TypedResults.Ok(new MovieListResponseDTO("success", new List<Movie>(){movie}));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Manager")]
        public static async Task<IResult> UpdateMovie(int id, UpdateMoviePayload payload, ICinemaRepository repository)
        {
            if (string.IsNullOrEmpty(payload.Title))
            {
                return TypedResults.BadRequest("Title is required");
            }
            if (string.IsNullOrEmpty(payload.Rating))
            {
                return TypedResults.BadRequest("Rating is required");
            }
            if (string.IsNullOrEmpty(payload.Description))
            {
                return TypedResults.BadRequest("Description is required");
            }
            if (payload.RunTimeMinutes <= 0)
            {
                return TypedResults.BadRequest("RunTimeMinutes must be greater than 0");
            }
            var movie = await repository.UpdateMovie(id, payload);
            if (movie == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(new MovieListResponseDTO("success", new List<Movie>(){movie}));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public static async Task<IResult> DeleteMovie(int id, ICinemaRepository repository)
        {
            var deleted = await repository.DeleteMovie(id);
            if (!deleted)
            {
                return TypedResults.NotFound();
            }
            var movies = await repository.GetAllMovies();
            return TypedResults.Ok(new MovieListResponseDTO("success", movies));;
        }
    }
}