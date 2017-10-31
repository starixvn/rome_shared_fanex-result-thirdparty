namespace Fanex.Result.ThirdParty.Models
{
    public class Auth
    {
        public Auth(
            string login,
            string password,
            int siteId)
        {
            Login = login;
            Password = password;
            SiteId = siteId;
        }

        public string Login { get; }

        public string Password { get; }

        public int SiteId { get; }
    }
}
