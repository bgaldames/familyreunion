using System;

namespace FamilyReunion.Models
{
    public class Duty
    {
        public int DutyId { get; set; }
        public int DutyTypeId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Team Team { get; set; }
        public virtual DutyType DutyType { get; set; }
    }
}
