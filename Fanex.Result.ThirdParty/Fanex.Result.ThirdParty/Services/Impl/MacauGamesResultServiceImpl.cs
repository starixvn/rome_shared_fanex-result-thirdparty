namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Core.Voidbridge;
    using Helpers;
    using Models;
    using Models.Requests;

    public class MacauGamesResultServiceImpl : ThirdPartyResultService
    {
        private readonly VoidbridgeApi voidbridgeApi;

        public MacauGamesResultServiceImpl(VoidbridgeApi voidbridgeApi = null)
        {
            this.voidbridgeApi = voidbridgeApi ?? new VoidbridgeApiImp();
        }

        public ThirdPartyResult GetResultLink(ThirdPartyResultQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

                var resultLink = voidbridgeApi.GetBetHistoryUrl(
                    request.CustId.ToString(),
                    request.RefNo,
                    request.Language,
                    siteId.ToString());

                return new ThirdPartyResult(true, resultLink, string.Empty);
            }
            catch (Exception ex)
            {
                return new ThirdPartyResult(false, string.Empty, ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}
