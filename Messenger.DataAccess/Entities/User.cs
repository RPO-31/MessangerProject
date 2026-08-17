namespace Messenger.DataAccess.Classes
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string OutputName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
