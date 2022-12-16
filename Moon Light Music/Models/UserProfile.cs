namespace Moon_Light_Music.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public string Id { get; set; } = null!;
        public string? Description
        {
            get; set;
        }
        public string? Name
        {
            get; set;
        }
        public string? Cccd
        {
            get; set;
        }
        public string? Address
        {
            get; set;
        }
        public string? Images
        {
            get; set;
        }
        public string? Sex
        {
            get; set;
        }
        public string? Country
        {
            get; set;
        }
        public DateTime? Birthday
        {
            get; set;
        }
        public string? Avatar
        {
            get; set;
        }

        public virtual ICollection<UserAccount> UserAccounts
        {
            get; set;
        }
    }
}
