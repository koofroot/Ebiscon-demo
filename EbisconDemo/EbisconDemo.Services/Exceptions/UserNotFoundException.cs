namespace EbisconDemo.Services.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
        {            
        }
        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
