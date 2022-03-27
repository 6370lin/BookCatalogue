using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using PublicApi.Requests;
using PublicApi.Responses;
using FastEndpoints;

namespace PublicApi.AuthEndPoints
{
    public class AuthenticateEndpoint : Endpoint<AuthenticateRequest, AuthenticateResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;

        public AuthenticateEndpoint(SignInManager<ApplicationUser> signInManager,
                                    ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("authenticate");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new AuthenticateResponse();

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

            response.Result = result.Succeeded;
            response.IsLockedOut = result.IsLockedOut;
            response.IsNotAllowed = result.IsNotAllowed;
            response.RequiresTwoFactor = result.RequiresTwoFactor;
            response.Username = request.Username;

            if (result.Succeeded)
            {
                response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);
            }

            await SendAsync(response, cancellation: cancellationToken);
        }
    }
}
