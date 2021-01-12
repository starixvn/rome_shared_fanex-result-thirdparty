namespace Fanex.Result.ThirdParty.Models.Requests
{
    public class ThirdPartyResultBaseQuery
    {
        public int CustId { get; set; }

        public int RoleId { get; set; }

        public int ProductId { get; set; }

        public string GameId { get; set; }

        public string GameCode { get; set; }

        public string Language { get; set; }

        public string SiteId { get; set; }
    }
}
