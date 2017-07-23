namespace FamilyReunion.Models
{
    // Models returned by MeController actions.
    public class GetViewModel
    {
        public string Hometown { get; set; }
        public string Email { get; set; }
        public Member Member { get; set; }
    }
}