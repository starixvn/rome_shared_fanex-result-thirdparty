namespace Fanex.Result.ThirdParty.Helpers
{
    using Sunplus.Agent.Model.BL.Definitions;

    public static class SiteIdHelper
    {
        public static int GetSiteIdByRole(int roleId) =>
            roleId.Equals(UserTypes.Agent) ? 3000 :
            roleId.Equals(UserTypes.Master) ? 3100 :
            3200;
    }
}
