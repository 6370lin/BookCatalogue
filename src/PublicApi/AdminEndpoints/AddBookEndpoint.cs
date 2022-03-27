using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PublicApi.Requests;
using PublicApi.Responses;

namespace PublicApi.AdminEndpoints
{
    public class AddBookEndPoint : Endpoint<AddBookRequest, AddBookResponse>
    {
        private readonly IRepository<Book> _bookrepository;
        public AddBookEndPoint(IRepository<Book> bookrepository)
        {
            _bookrepository =  bookrepository;
    }

        public override void Configure()
        {
            
            Verbs(Http.POST);
            AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
            Routes("admin/add-book");
            Roles("Admin");
        }

        public override async Task HandleAsync(AddBookRequest request, CancellationToken cancellationToken = default)
        {
            var response = new AddBookResponse();

            await _bookrepository.AddAsync(
                                 new Book(
                                     request.Isbn,
                                     request.description,
                                     request.title,
                                     request.text,
                                     decimal.Parse(request.subscriptionPrice),
                                     request.pictureUri)
                                  ,cancellationToken);
             
            await _bookrepository.SaveChangesAsync(cancellationToken);

            response.Success= true;

            await SendAsync(response, cancellation: cancellationToken);
        }
    }
}
