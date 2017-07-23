using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyReunion.Models
{
    public class Member
    {
        public Member()
        {
            FamilyMemberships = new List<FamilyMember>();
        }
        public int MemberId { get; set; }
        public int MemberTypeId { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string CellPhone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsEligibleForWork { get; set; }

        public virtual MemberType MemberType { get; set; }
        public virtual ICollection<FamilyMember> FamilyMemberships { get; set; }
    }
}
