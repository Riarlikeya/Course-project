namespace CarSharing.Models
{
    public partial class User
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Login { get; set; } = null!;

        public string? Email { get; set; }

        public string Phone { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? DriverPass { get; set; }

        public int RoleId { get; set; }

        public int? SubId { get; set; }

        public virtual ICollection<ReservedCar> ReservedCars { get; set; } = new List<ReservedCar>();

        public virtual Role Role { get; set; } = null!;

        public virtual Subscription? Sub { get; set; }
    }
}
