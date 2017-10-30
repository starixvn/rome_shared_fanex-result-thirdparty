namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Core.Base.Models;
    using Core.GlobalGaming;
    using Core.GlobalGaming.DTO;
    using Helpers;
    using Models;
    using Models.Requests;

    public class FishingWorldResultServiceImpl : ResultService
    {
        private static readonly Dictionary<int, string> SiteIdMaps = new Dictionary<int, string>
        {
            [RoleIdConstants.Super] = "3200",
            [RoleIdConstants.Master] = "3100",
            [RoleIdConstants.Agent] = "3000"
        };
        private readonly Base3rdResultApi resultApi;

        public FishingWorldResultServiceImpl(Base3rdResultApi resultApi = null)
        {
            this.resultApi = resultApi ?? new GlobalGamingResultApi();
        }

        public Result GetResultLink(ThirdPartyResultQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId, SiteIdMaps, SiteIdMaps[RoleIdConstants.Super]);

                var globalGamingDTO = new GlobalGamingDTO
                {
                    CustId = request.CustId.ToString(),
                    GameId = request.RefNo,
                    SiteId = siteId,
                    Language = request.Language
                };

                var resultLink = resultApi.GetResultUrl(globalGamingDTO);

                return new SuccessResult(resultLink);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
