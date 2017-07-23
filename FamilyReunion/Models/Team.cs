using System.Collections.Generic;

namespace FamilyReunion.Models
{
    public class Team
    {
        public Team()
        {
            Members = new List<Member>();
            Duties = new List<Duty>();
        }
        public int TeamId { get; set; }
        public int ReunionId { get; set; }
        public int? TeamLead { get; set; }

        public ICollection<Member> Members { get; set; }
        public ICollection<Duty> Duties { get; set; }
    }
}
