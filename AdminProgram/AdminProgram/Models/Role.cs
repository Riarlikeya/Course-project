using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProgram.Models
{
    public partial class Role
    {

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
