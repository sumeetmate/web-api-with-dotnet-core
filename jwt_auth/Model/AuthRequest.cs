namespace jwt_auth.Model
{
    public class AuthRequest
    {
        public string Email { get; set; } = String.Empty; 
        public string Password { get; set; } = String.Empty;
    }
}
