namespace CarSharing.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
