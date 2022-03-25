using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BookCatalogueViewModelService : IBookCatalogViewModelService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IUriComposer _uriComposer;

        public BookCatalogueViewModelService(IRepository<Book> bookRepository,
                                             IUriComposer uriComposer)
        {
            _bookRepository = bookRepository;
            _uriComposer = uriComposer;
        }

        public async Task<CatalogueViewModel> GetBookCatalogueViewModelAsync()
        {
            var catalogViewModel = new CatalogueViewModel();

            catalogViewModel.CatalogItems = (await _bookRepository.ListAsync())
                                            .Select(b => 
                                             new CatalogItemViewModel 
                                             { 
                                                 Id = b.Isbn,
                                                 Title = b.Title,
                                                 Description = b.Description,
                                                 Price = b.SubscriptionPriceDisplay,
                                                 PictureUri = _uriComposer.ComposePicUri(b.PictureUri)                                                
                                             })
                                            .ToList();

            catalogViewModel.PaginationInfo = new PaginationInfoViewModel 
            {
                ItemsPerRow = 3,
                TotalItems = catalogViewModel.CatalogItems.Count
            };

            return catalogViewModel;
        }
    }
}
