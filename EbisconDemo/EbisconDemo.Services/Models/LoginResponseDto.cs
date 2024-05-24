namespace EbisconDemo.Services.Models
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
