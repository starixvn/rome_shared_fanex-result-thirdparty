namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Core.Base.Models;
    using Core.GlobalGaming;
    using Core.GlobalGaming.DTO;
    using Helpers;
    using Models;
    using Models.Requests;

    public class FishingWorldResultServiceImpl : ThirdPartyResultService
    {
        private readonly Base3rdResultApi resultApi;

        public FishingWorldResultServiceImpl(Base3rdResultApi resultApi = null)
        {
            this.resultApi = resultApi ?? new GlobalGamingResultApi();
        }

        public ThirdPartyResult GetResultLink(ThirdPartyResultQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);
                var globalGamingDTO = new GlobalGamingDTO
                {
                    CustId = request.CustId.ToString(),
                    GameId = request.RefNo,
                    SiteId = siteId,
                    Language = request.Language
                };

                var resultLink = resultApi.GetResultUrl(globalGamingDTO);

                return new ThirdPartyResult(true, resultLink, string.Empty);
            }
            catch (Exception ex)
            {
                return new ThirdPartyResult(false, string.Empty, ex.Message);
            }
        }
    }
}
