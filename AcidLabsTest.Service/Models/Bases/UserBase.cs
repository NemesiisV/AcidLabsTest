namespace AcidLabsService.Models.Bases
{
    public abstract class UserBase
    {
        public string Rut { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
