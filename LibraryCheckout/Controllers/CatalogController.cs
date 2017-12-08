using LibraryCheckout.Models.Catalog;
using LibraryData;
using LibraryServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryCheckout.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets;
        public CatalogController(ILibraryAsset assets)
        {
            _assets = assets;
        }
        public IActionResult Index()
        {
            var assetModels = _assets.GetAll();

            var listingResult = assetModels
                .Select(result => new Models.Catalog.AssetIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    AuthorOrDirector = _assets.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _assets.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _assets.GetType(result.Id)

                });
            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };
            return View(model);
        }
    }
}
