using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FastEndpoints;

namespace PublicApi.CatalogEndpoints
{
    public class CatalogListItemsEndpoint : EndpointWithoutRequest
    {
        private readonly IRepository<Book> _bookrepository;
        public CatalogListItemsEndpoint(IRepository<Book> bookrepository)
        {
            _bookrepository = bookrepository;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("list-catalog");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendAsync(GetCatalogItemsAsync());
        }

        private async Task<List<CatalogItemDto>> GetCatalogItemsAsync()
        {
            return (await _bookrepository.ListAsync())
                .Select(b => 
                new CatalogItemDto 
                { 
                    ISBN = b.Isbn,
                    Description = b.Description,
                    Title = b.Title,
                    PurchasePrice = b.SubscriptionPriceDisplay
                }).ToList();
        }
    }
}
