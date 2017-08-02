namespace FamilyReunion.Models
{
    public class ReunionMember
    {
        public int ReunionMemberId { get; set; }
        public int MemberId { get; set; }
        public int ReunionId { get; set; }
        public bool IsAttending { get; set; }
        public string Reason { get; set; }

        public virtual Member Member { get; set; }
        public virtual Reunion Reunion { get; set; }
    }
}