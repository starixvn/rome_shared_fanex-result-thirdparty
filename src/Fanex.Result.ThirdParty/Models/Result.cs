namespace Fanex.Result.ThirdParty.Models
{
    public abstract class Result
    {
        protected Result(
            bool isSuccess,
            string value)
        {
            IsSuccess = isSuccess;
            Value = value;
        }

        public bool IsSuccess { get; }

        public string Value { get; }
    }

    public class SuccessResult : Result
    {
        public SuccessResult(string resultLink)
            : base(true, resultLink)
        {
        }
    }

    public class ErrorResult : Result
    {
        public ErrorResult(string resultMessage)
            : base(false, resultMessage)
        {
        }
    }
}
