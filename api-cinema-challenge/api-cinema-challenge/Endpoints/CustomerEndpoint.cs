using api_cinema_challenge.DTO;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.Endpoints
{
    public static class CustomerEndpoint
    {
        public static void ConfigureCustomerEndpoint(this WebApplication app)
        {
            var cinema = app.MapGroup("/customers");
            cinema.MapGet("", GetAllCustomers);
            cinema.MapPost("", CreateCustomer);
            cinema.MapPut("/{id}", UpdateCustomer);
            cinema.MapDelete("/{id}", DeleteCustomer);
        }

        public static async Task<IResult> GetAllCustomers(ICinemaRepository repository)
        {
            var customers = await repository.GetAllCustomers();
            return TypedResults.Ok(new CustomerResponseDTO("success", customers));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public static async Task<IResult> CreateCustomer(CreateCustomerPayload payload, ICinemaRepository repository)
        {
            if (string.IsNullOrEmpty(payload.Name))
            {
                return TypedResults.BadRequest("Name is required");
            }
            if (string.IsNullOrEmpty(payload.Email))
            {
                return TypedResults.BadRequest("Email is required");
            }
            if (string.IsNullOrEmpty(payload.Phone))
            {
                return TypedResults.BadRequest("Phone is required");
            }
            var customer = await repository.CreateCustomer(payload);
            return TypedResults.Ok(new CustomerResponseDTO("success", new List<Customer>(){customer}));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public static async Task<IResult> UpdateCustomer(int id, UpdateCustomerPayload payload, ICinemaRepository repository)
        {
            if (string.IsNullOrEmpty(payload.Name))
            {
                return TypedResults.BadRequest("Name is required");
            }
            if (string.IsNullOrEmpty(payload.Email))
            {
                return TypedResults.BadRequest("Email is required");
            }
            if (string.IsNullOrEmpty(payload.Phone))
            {
                return TypedResults.BadRequest("Phone is required");
            }
            var customer = await repository.UpdateCustomer(id, payload);
            if (customer == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(new CustomerResponseDTO("success", new List<Customer>(){customer}));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public static async Task<IResult> DeleteCustomer(int id, ICinemaRepository repository)
        {
            var deleted = await repository.DeleteCustomer(id);
            if (!deleted)
            {
                return TypedResults.NotFound();
            }
            var customers = await repository.GetAllCustomers();
            return TypedResults.Ok(new CustomerResponseDTO("success", customers));
        }
    }
}