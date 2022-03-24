using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BookCatalogueViewModelService : IBookCatalogViewModelService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookCatalogueViewModelService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
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
                                                 PictureUri = b.PictureUri                                                 
                                             })
                                            .ToList();

            return catalogViewModel;
        }
    }
}
