namespace Fanex.Result.ThirdParty.Helpers
{
    using System.Collections.Generic;
    using Constants;

    public static class SiteHelper
    {
        private static readonly Dictionary<int, string> DefaultSiteIdMaps = new Dictionary<int, string>
        {
            [RoleIdConstants.Super] = "3200",
            [RoleIdConstants.Master] = "3100",
            [RoleIdConstants.Agent] = "3000"
        };

        public static string GetSiteIdByRole(int roleId, IDictionary<int, string> siteIdMaps = null, string defaultSiteId = null)
        {
            var maps = siteIdMaps ?? DefaultSiteIdMaps;

            return maps.ContainsKey(roleId)
                ? maps[roleId]
                : (defaultSiteId ?? maps[RoleIdConstants.Super]);
        }
    }
}
