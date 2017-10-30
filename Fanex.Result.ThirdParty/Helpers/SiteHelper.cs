namespace Fanex.Result.ThirdParty.Helpers
{
    using System.Collections.Generic;
    using Sunplus.Agent.Model.BL.Definitions;

    public static class SiteHelper
    {
        private static readonly IDictionary<int, string> SideIdMaps = new Dictionary<int, string>()
        {
            [(int)UserTypes.Agent] = "3000",
            [(int)UserTypes.Master] = "3100",
            [(int)UserTypes.Super] = "3200"
        };
        private const string DefaultSiteId = "3200";

        public static string GetSiteIdByRole(int roleId)
            => SideIdMaps.ContainsKey(roleId) ? SideIdMaps[roleId] : DefaultSiteId;


    }
}
