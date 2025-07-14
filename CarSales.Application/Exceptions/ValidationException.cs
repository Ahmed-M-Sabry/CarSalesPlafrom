namespace CarSales.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> Errors { get; }

        public CustomValidationException(List<string> errors)
            : base("Validation failed.")
        {
            Errors = errors;
        }
    }
}
