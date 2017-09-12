using System;
using System.Collections.Generic;

namespace FamilyReunion.Models
{
    public class Family
    {
        public Family()
        {
            FamilyMembers = new List<FamilyMember>();
        }
        public int FamilyId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set;}
    }
}
