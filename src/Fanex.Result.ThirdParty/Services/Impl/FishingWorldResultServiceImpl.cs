namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Core.Base.Models;
    using Core.GlobalGaming;
    using Core.GlobalGaming.DTO;
    using Helpers;
    using Models;
    using Models.Requests;

    public class FishingWorldResultServiceImpl : ResultService
    {
        private readonly Base3rdResultApi resultApi;

        public FishingWorldResultServiceImpl(Base3rdResultApi resultApi = null)
        {
            this.resultApi = resultApi ?? new GlobalGamingResultApi();
        }

        public Result GetResultLink(ThirdPartyResultBaseQuery request)
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

                return new SuccessResult(resultLink);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
