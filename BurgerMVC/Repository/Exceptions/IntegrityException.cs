namespace BurgerMVC.Repository.Exceptions
{
    public class IntegrityException : Exception
    {
        public IntegrityException(string message) : base(message)
        { }
    }
}
