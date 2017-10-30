namespace Fanex.Result.ThirdParty.Models
{
    public abstract class ThirdPartyResult
    {
        protected ThirdPartyResult(
            bool isSuccess,
            string result)
        {
            IsSuccess = isSuccess;
            Result = result;
        }

        public bool IsSuccess { get; }

        public string Result { get; }
    }
}
