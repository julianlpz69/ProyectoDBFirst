using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class Driver
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }
    public ICollection<TeamDriver> TeamDrivers {get; set;}

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
}