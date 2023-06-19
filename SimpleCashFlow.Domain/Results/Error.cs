namespace SimpleCashFlow.Domain.Results
{
    public class Error
    {
        public static readonly Error NoError = new Error(string.Empty, string.Empty);


        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

        public static implicit operator string(Error error) => error.Code;

    }
}
