using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models
{
    [Table("tickets")]
    public class Ticket
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("seat_number")]
        public int SeatNumber { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("screening_id")]
        public int ScreeningId { get; set; }
        
        public Screening Screening { get; set; }
        public Customer Customer { get; set; }
    }
}