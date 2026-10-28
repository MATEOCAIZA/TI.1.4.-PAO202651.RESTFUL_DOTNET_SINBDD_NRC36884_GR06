namespace ec.edu.monster.modelo
{
    public class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Usuario()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public Usuario(string username, string password)
        {
            Username = username ?? string.Empty;
            Password = password ?? string.Empty;
        }
    }
}
