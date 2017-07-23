namespace FamilyReunion.Models
{
    public class FamilyMember
    {
        public int FamilyMemberId { get; set; }
        public int FamilyId { get; set; }
        public int MemberId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
