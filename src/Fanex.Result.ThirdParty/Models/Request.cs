namespace Fanex.Result.ThirdParty.Models
{
    public class Request
    {
        public string Timestamp { get; set; }

        public Auth Auth { get; set; }

        public Call Call { get; set; }
    }
}
