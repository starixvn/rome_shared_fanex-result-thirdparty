namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Core.Voidbridge;
    using Helpers;
    using Models;
    using Models.Requests;

    public class MacauGamesResultServiceImpl : ResultService
    {
        private static readonly Dictionary<int, string> SiteIdMaps = new Dictionary<int, string>
        {
            [RoleIdConstants.Super] = "3200",
            [RoleIdConstants.Master] = "3100",
            [RoleIdConstants.Agent] = "3000"
        };
        private readonly VoidbridgeApi voidbridgeApi;

        public MacauGamesResultServiceImpl(VoidbridgeApi voidbridgeApi = null)
        {
            this.voidbridgeApi = voidbridgeApi ?? new VoidbridgeApiImp();
        }

        public ThirdPartyResult GetResultLink(ThirdPartyResultQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId, SiteIdMaps, SiteIdMaps[RoleIdConstants.Super]);

                var resultLink = voidbridgeApi.GetBetHistoryUrl(
                    request.CustId.ToString(),
                    request.RefNo,
                    request.Language,
                    siteId.ToString());

                return new SuccessThirdPartyResult(resultLink);
            }
            catch (Exception ex)
            {
                return new ErrorThirdPartyResult(ex.Message);
            }
        }
    }
}
