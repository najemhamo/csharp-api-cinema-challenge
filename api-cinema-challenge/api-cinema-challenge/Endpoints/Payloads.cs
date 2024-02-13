using api_cinema_challenge.UserRoles;

namespace api_cinema_challenge.Endpoints
{
    // Payloads are used to pass data from the client to the server.
    public record CreateCustomerPayload(string Name, string Email, string Phone);
    public record UpdateCustomerPayload(string Name, string Email, string Phone);

    public record CreateMoviePayload(string Title, string Rating, string Description, int RunTimeMinutes);
    public record UpdateMoviePayload(string Title, string Rating, string Description, int RunTimeMinutes);
    public record CreateScreeningPayload(int MovieId, int ScreenNumber, int Capacity, DateTime StartTime);
    
    public record CreateTicketPayload(int SeatNumber, int CustomerId, int ScreeningId);


    public record RegisterDto(string Email, string Password);
    public record LoginDto(string Email, string Password);
    public record AuthResponseDto(string Token, string Email, Roles Role);

}