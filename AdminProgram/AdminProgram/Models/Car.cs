using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProgram.Models
{
    public partial class Car
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string StateNumber { get; set; } = null!;

        public decimal Price { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int StatusId { get; set; }

        public virtual ICollection<ReservedCar> ReservedCars { get; set; } = new List<ReservedCar>();

        public virtual Status Status { get; set; } = null!;
    }
}
