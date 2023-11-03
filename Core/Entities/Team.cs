using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class Team
    {
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public ICollection<TeamDriver> TeamDrivers {get; set;}
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    }
}