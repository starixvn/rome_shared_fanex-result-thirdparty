namespace Fanex.Result.ThirdParty.Helpers
{
    using System.Collections.Generic;

    public static class SiteHelper
    {
        public static string GetSiteIdByRole(int roleId, IDictionary<int, string> siteIdMaps, string defaultSiteId)
            => siteIdMaps.ContainsKey(roleId) ? siteIdMaps[roleId] : defaultSiteId;
    }
}
