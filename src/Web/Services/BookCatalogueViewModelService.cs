using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Books;
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

        public async Task<CatalogueViewModel> GetBookCatalogueViewModelAsync(string userid = "")
        {
            var catalogViewModel = new CatalogueViewModel();

            var bookWithSubscriptionsSpec = new BooksWithSubscriptionsSpecification();

            catalogViewModel.CatalogItems = (await _bookRepository.ListAsync(bookWithSubscriptionsSpec))
                                            .Select(b => 
                                             new CatalogItemViewModel 
                                             { 
                                                 Id = b.Isbn,
                                                 Title = b.Title,
                                                 Description = b.Description,
                                                 Price = b.SubscriptionPriceDisplay,
                                                 PictureUri = _uriComposer.ComposePicUri(b.PictureUri),
                                                 BtnMode = SubscriptionBtnHandler(b.Subscriptions, userid)
                                             })
                                            .ToList();

            catalogViewModel.PaginationInfo = new PaginationInfoViewModel 
            {
                ItemsPerRow = 3,
                TotalItems = catalogViewModel.CatalogItems.Count
            };

            return catalogViewModel;
        }

        public SubscriptionBtnMode SubscriptionBtnHandler(ICollection<Subscription> Subscriptions, string userid)
        {
            //user not logged in
            if (string.IsNullOrEmpty(userid))
            {
                return SubscriptionBtnMode.Disable;
            }

            if (Subscriptions.Any())
            {
                //user already subscribed
                if (Subscriptions.Select(sub => sub.UserId).Contains(userid))
                {
                    return SubscriptionBtnMode.Subscribed;
                }
            }

            return SubscriptionBtnMode.Subscribe;
        }
    }
}
