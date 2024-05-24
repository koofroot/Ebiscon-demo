namespace EbisconDemo.Services.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException()
        {            
        }
        protected NotFoundException(string message)
            :base(message)
        {            
        }
    }
}
