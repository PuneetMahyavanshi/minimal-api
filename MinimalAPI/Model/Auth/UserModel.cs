namespace MinimalAPI.Model.Auth
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }
        
        public string Password { get; set; }

        public string Role { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
