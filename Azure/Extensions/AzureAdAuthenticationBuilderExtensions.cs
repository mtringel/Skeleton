using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Client;
using TopTal.JoggingApp.Azure.Principals;
using TopTal.JoggingApp.Configuration;

namespace TopTal.JoggingApp.Azure.Extensions
{
    /// <summary>
    /// Microsoft Graph Connect Sample for ASP.NET Core 2.0
    /// https://github.com/microsoftgraph/aspnetcore-connect-sample
    /// 
    /// Additional info:
    /// https://forums.asp.net/t/2125981.aspx?Having+issue+with+my+Microsoft+Identity+Client+ConfidentialClientApplication+object+having+empty+Users+property
    /// 
    /// Copyright (c) 2017 Microsoft. All rights reserved.
    /// </summary>
    public static class AzureAdAuthenticationBuilderExtensions
    {
        // [mtringel]
        //public static AuthenticationBuilder AddAzureAd(this AuthenticationBuilder builder)
        //    => builder.AddAzureAd(_ => { });

        public static AuthenticationBuilder AddAzureAd(this AuthenticationBuilder builder) // , Action<AzureAdOptions> configureOptions)
        {
            //builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<OpenIdConnectOptions>, ConfigureAzureOptions>();
            builder.AddOpenIdConnect();
            return builder;
        }

        public class ConfigureAzureOptions: IConfigureNamedOptions<OpenIdConnectOptions>
        {
            // [mtringel]
            //private readonly AzureAdOptions _azureOptions;
            //public AzureAdOptions GetAzureAdOptions() => _azureOptions;

            public ConfigureAzureOptions(AppConfig appConfig) // IOptions<AzureAdOptions> azureOptions)
            {
                // [mtringel]
                //_azureOptions = azureOptions.Value;
                this.AppConfig = appConfig;
            }

            private AppConfig AppConfig;

            public void Configure(string name, OpenIdConnectOptions options)
            {
                // [mtringel]
                var _azureOptions = AppConfig.AzureAdOptions;

                options.ClientId = _azureOptions.ClientId;
                options.Authority = _azureOptions.Instance; // [mtringel] $"{_azureOptions.Instance}common/v2.0";
                options.UseTokenLifetime = true;
                options.CallbackPath = _azureOptions.CallbackPath;
                options.RequireHttpsMetadata = true;
                options.ClientSecret = _azureOptions.ClientSecret;

                // [mtringel]
                //options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                options.GetClaimsFromUserInfoEndpoint = true;
                //options.ResponseType = $"{OpenIdConnectResponseType.Code} {OpenIdConnectResponseType.IdToken}";
                options.ResponseMode = "form_post";
                options.SaveTokens = true;

                //var allScopes = $"{_azureOptions.Scopes} {_azureOptions.GraphScopes}".Split(new[] { ' ' });
                //foreach (var scope in allScopes) { options.Scope.Add(scope); }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Instead of using the default validation (validating against a single issuer value, as we do in line of business apps),
                    // we inject our own multitenant validation logic
                    ValidateIssuer = false,

                    // If the app is meant to be accessed by entire organizations, add your issuer validation logic here.
                    //IssuerValidator = (issuer, securityToken, validationParameters) => {
                    //    if (myIssuerValidationLogic(issuer)) return issuer;
                    //}

                    // [mtringel]
                    NameClaimType = "name"
                };

                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        // If your authentication logic is based on users then add your logic here
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.Redirect("/Home/Error");
                        context.HandleResponse(); // Suppress the exception
                        return Task.CompletedTask;
                    }
                    // If your application needs to do authenticate single users, add your user validation below.
                    //OnTokenValidated = context =>
                    //{
                    //    return myUserValidationLogic(context.Ticket.Principal);
                    //}
                };
            }

            public void Configure(OpenIdConnectOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}
