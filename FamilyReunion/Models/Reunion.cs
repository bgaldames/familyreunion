using System.Collections.Generic;

namespace FamilyReunion.Models
{
    public class Reunion
    {
        public Reunion()
        {
            Teams = new List<Team>();
        }
        public int ReunionId { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
