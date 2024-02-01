namespace CarSharing.Models
{
    public partial class ReservedCar
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int CarId { get; set; }

        public int BookingStatus { get; set; }

        public virtual Status BookingStatusNavigation { get; set; } = null!;

        public virtual Car Car { get; set; } = null!;

        public virtual User? User { get; set; }
    }

}
