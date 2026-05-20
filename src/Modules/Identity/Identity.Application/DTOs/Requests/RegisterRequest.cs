namespace Identity.Application.DTOs.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
