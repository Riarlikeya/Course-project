using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProgram.Models
{
    public partial class Status
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

        public virtual ICollection<ReservedCar> ReservedCars { get; set; } = new List<ReservedCar>();
    }
}
