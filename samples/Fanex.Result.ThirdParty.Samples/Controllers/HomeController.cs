namespace Fanex.Result.ThirdParty.Samples.Controllers
{
    using System.Web.Mvc;
    using Common.Helper;
    using Logging;
    using Models.Requests;
    using Models.Views;
    using Services;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;

    public class HomeController : Controller
    {
        private readonly ProductResultServiceFactory serviceFactory;

        public HomeController()
        {
            var jsonCoverver = new JsonConverterImpl();
            serviceFactory = new ProductResultServiceFactoryImpl(Logger.Log, jsonCoverver);
        }

        // GET: Home
        public ActionResult Index(ThirdPartyResultBaseQuery message)
        {
            Check.Argument.IsNotNull(message, "message");

            var service = serviceFactory.CreateProductResultService(message.ProductId);

            var viewModel = new ThirdPartyResultViewModel("Product Name", service.GetResultLink(message));

            return View(viewModel);
        }
    }
}
