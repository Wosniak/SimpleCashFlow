namespace SimpleCashFlow.Domain.Results
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.NoError)
            {
                throw new ArgumentException("Return Cannot be sucessfull with an Error");
            }
            if (!isSuccess && error == Error.NoError)
            {
                throw new ArgumentException("All uncessfull return must have an Error");
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public Error Error { get; }

        public static Result Success() => new(true, Error.NoError);

        public static Result Fail(Error error) => new(false, error);

        public static Result<TReturn> Fail<TReturn>(Error error) => new(error);

        public bool HasValidationError { get; private set; }

        public IEnumerable<Error>? Messages { get; set; }

        public static Result ValidationFailResult(IEnumerable<Error> errors)
        {
            var result = new Result(false, new Error("SimpleCashFlow.Validation.Error", "There are some validations errors. Check the Messages list property to validate then."))
            {
                Messages = errors
            };
            result.HasValidationError = true;
            return result;
        }

        public static Result<TReturn> ValidationFailResult<TReturn>(IEnumerable<Error> errors) => new(new Error("SimpleCashFlow.Validation.Error", "There are some validations errors. Check the Messages list property to validate then."))
        {
            HasValidationError = true,
            Messages = errors
        };

    }


    public class Result<TReturn> : Result
    {
        private readonly TReturn? _returnValue;

        protected internal Result(Error error) : base(false, error) { }

        protected internal Result(TReturn returnValue, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _returnValue = returnValue;
        }

        public TReturn Value => _returnValue!;

        public static implicit operator Result<TReturn>(TReturn? returnValue) => Create(returnValue);


        private static Result<TReturn> Create(TReturn? returnValue)
        {
            return new Result<TReturn>(returnValue!, true, Error.NoError);
        }
    }

}
