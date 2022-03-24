using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBookCatalogViewModelService
    {
        Task<CatalogueViewModel> GetBookCatalogueViewModelAsync();
    }
}
