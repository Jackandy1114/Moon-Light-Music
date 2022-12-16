namespace Moon_Light_Music.Models
{
    public partial class UserAccount
    {
        public int Id
        {
            get; set;
        }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber
        {
            get; set;
        }
        public string? Devices
        {
            get; set;
        }
        public string UserProfile { get; set; } = null!;
        public string? Role
        {
            get; set;
        }

        public virtual UserProfile UserProfileNavigation { get; set; } = null!;
    }
}
