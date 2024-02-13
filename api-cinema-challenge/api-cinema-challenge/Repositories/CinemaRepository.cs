using api_cinema_challenge.Models;
using api_cinema_challenge.Endpoints;
using api_cinema_challenge.Data;
using Microsoft.EntityFrameworkCore;


namespace api_cinema_challenge.Repositories
{
    public class CinemaRepository: ICinemaRepository
    {
        private CinemaContext _context;

        public CinemaRepository(CinemaContext db)
        {
            _context = db;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> CreateCustomer(CreateCustomerPayload payload)
        {
            DateTime utc = DateTime.Now.ToUniversalTime();
            var customer = new Customer
            {
                Name = payload.Name,
                Email = payload.Email,
                Phone = payload.Phone,
                CreatedAt = utc,
                UpdatedAt = utc
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> UpdateCustomer(int id, UpdateCustomerPayload payload)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null || string.IsNullOrEmpty(payload.Name) || string.IsNullOrEmpty(payload.Email) || string.IsNullOrEmpty(payload.Phone))
            {
                return null;
            }

            DateTime utc = DateTime.Now.ToUniversalTime();

            customer.Name = payload.Name;
            customer.Email = payload.Email;
            customer.Phone = payload.Phone;
            customer.UpdatedAt = utc;

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> CreateMovie(CreateMoviePayload payload)
        {
            DateTime utc = DateTime.Now.ToUniversalTime();
            var movie = new Movie
            {
                Title = payload.Title,
                Rating = payload.Rating,
                Description = payload.Description,
                RunTimeMinutes = payload.RunTimeMinutes,
                CreatedAt = utc,
                UpdatedAt = utc
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie?> UpdateMovie(int id, UpdateMoviePayload payload)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return null;
            }
            DateTime utc = DateTime.Now.ToUniversalTime();
            movie.Title = payload.Title;
            movie.Rating = payload.Rating;
            movie.Description = payload.Description;
            movie.RunTimeMinutes = payload.RunTimeMinutes;
            movie.UpdatedAt = utc;

            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<bool> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<ICollection<Screening>> GetAllScreeningsByMovieID(int movieId)
        {
            if (movieId < 0)
            {
                return null;
            }
            var screenings = await _context.Screenings.Where(s => s.MovieId == movieId).ToListAsync();
            return screenings;
        }


        public async Task<Screening?> CreateScreening(CreateScreeningPayload payload)
        {
            DateTime utc = DateTime.Now.ToUniversalTime();
            var screening = new Screening
            {
                MovieId = payload.MovieId,
                ScreenNumber = payload.ScreenNumber,
                Capacity = payload.Capacity,
                StartTime = payload.StartTime,
                CreatedAt = utc,
                UpdatedAt = utc
            };

            await _context.Screenings.AddAsync(screening);
            await _context.SaveChangesAsync();
            return screening;
        }

        public async Task<Ticket?> CreateTicket(CreateTicketPayload payload)
        {
            DateTime utc = DateTime.Now.ToUniversalTime();
            var ticket = new Ticket
            {
                SeatNumber = payload.SeatNumber,
                CustomerId = payload.CustomerId,
                ScreeningId = payload.ScreeningId,
                CreatedAt = utc,
                UpdatedAt = utc
            };

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<ICollection<Ticket>> GetAllTicketsByCustomerAndScreeningID(int customerId, int screeningId)
        {
            if (customerId == null || screeningId == null)
            {
                return null;
            }
            var tickets = await _context.Tickets.Where(t => t.CustomerId == customerId && t.ScreeningId == screeningId).ToListAsync();
            return tickets;
        }

        public async Task<ICollection<Ticket>> GetAllTicketsByCustomerID(int customerId)
        {
            if (customerId == null)
            {
                return null;
            }
            var tickets = await _context.Tickets.Where(t => t.CustomerId == customerId).ToListAsync();
            return tickets;
        }

        public async Task<ICollection<Ticket>> GetAllTicketsByScreeningID(int screeningId)
        {
            if (screeningId == null)
            {
                return null;
            }
            var tickets = await _context.Tickets.Where(t => t.ScreeningId == screeningId).ToListAsync();
            return tickets;
        }

        public ApplicationUser? GetUser(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
