namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Core.Voidbridge;
    using Helpers;
    using Models;
    using Models.Requests;

    public class MacauGamesResultServiceImpl : ResultService
    {
        private readonly VoidbridgeApi voidbridgeApi;

        public MacauGamesResultServiceImpl(VoidbridgeApi voidbridgeApi = null)
        {
            this.voidbridgeApi = voidbridgeApi ?? new VoidbridgeApiImp();
        }

        public Result GetResultLink(ThirdPartyResultBaseQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

                var resultLink = voidbridgeApi.GetBetHistoryUrl(
                    request.CustId.ToString(),
                    request.RefNo,
                    request.Language,
                    siteId.ToString());

                return new SuccessResult(resultLink);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
