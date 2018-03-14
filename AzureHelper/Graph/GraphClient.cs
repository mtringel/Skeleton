using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TopTal.JoggingApp.Configuration;

namespace TopTal.JoggingApp.AzureHelper.Graph
{
    /// <summary>
    /// A Console application for Azure AD B2C User Management the Azure AD Graph
    /// https://github.com/AzureADQuickStarts/B2C-GraphAPI-DotNet
    /// 
    /// From Azure AD Quick Starts
    /// https://github.com/AzureADQuickStarts
    /// </summary>
    public sealed class GraphClient
    {
        private AuthenticationContext authContext;

        private ClientCredential credential;

        private AppConfig AppConfig;

        private string TenantId;

        public GraphClient(AppConfig appConfig, string tenantId)
        {
            this.AppConfig = appConfig;
            this.TenantId = tenantId.Trim('/');

            // The client_id, client_secret, and tenant are pulled in from the App.config file
            // The AuthenticationContext is ADAL's primary class, in which you indicate the direcotry to use.
            this.authContext = new AuthenticationContext($"https://login.microsoftonline.com/{TenantId}");

            // The ClientCredential is where you pass in your client_id and client_secret, which are 
            // provided to Azure AD in order to receive an access_token using the app's identity.
            this.credential = new ClientCredential(AppConfig.AzureAdOptions.ClientId, AppConfig.AzureAdOptions.ClientSecret);
        }

        public async Task<string> GetUserByObjectId(string objectId)
        {
            return await SendGraphGetRequest("/users/" + objectId, null);
        }

        public async Task<string> GetAllUsers(string query)
        {
            return await SendGraphGetRequest("/users", query);
        }

        #region Not used

        //public async Task<string> CreateUser(string json)
        //{
        //    return await SendGraphPostRequest("/users", json);
        //}

        //public async Task<string> UpdateUser(string objectId, string json)
        //{
        //    return await SendGraphPatchRequest("/users/" + objectId, json);
        //}

        //public async Task<string> DeleteUser(string objectId)
        //{
        //    return await SendGraphDeleteRequest("/users/" + objectId);
        //}

        //public async Task<string> RegisterExtension(string objectId, string body)
        //{
        //    return await SendGraphPostRequest("/applications/" + objectId + "/extensionProperties", body);
        //}

        //public async Task<string> UnregisterExtension(string appObjectId, string extensionObjectId)
        //{
        //    return await SendGraphDeleteRequest("/applications/" + appObjectId + "/extensionProperties/" + extensionObjectId);
        //}

        //public async Task<string> GetExtensions(string appObjectId)
        //{
        //    return await SendGraphGetRequest("/applications/" + appObjectId + "/extensionProperties", null);
        //}

        //public async Task<string> GetApplications(string query)
        //{
        //    return await SendGraphGetRequest("/applications", query);
        //}

        //private async Task<string> SendGraphDeleteRequest(string api)
        //{
        //    // NOTE: This client uses ADAL v2, not ADAL v4
        //    AuthenticationResult result = authContext.AcquireToken(Globals.aadGraphResourceId, credential);
        //    HttpClient http = new HttpClient();
        //    string url = Globals.aadGraphEndpoint + tenant + api + "?" + Globals.aadGraphVersion;
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
        //    HttpResponseMessage response = await http.SendAsync(request);

        //    Console.ForegroundColor = ConsoleColor.Cyan;
        //    Console.WriteLine("DELETE " + url);
        //    Console.WriteLine("Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");
        //    Console.WriteLine("");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        string error = await response.Content.ReadAsStringAsync();
        //        object formatted = JsonConvert.DeserializeObject(error);
        //        throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
        //    }

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine((int)response.StatusCode + ": " + response.ReasonPhrase);
        //    Console.WriteLine("");

        //    return await response.Content.ReadAsStringAsync();
        //}

        //private async Task<string> SendGraphPatchRequest(string api, string json)
        //{
        //    // NOTE: This client uses ADAL v2, not ADAL v4
        //    AuthenticationResult result = authContext.AcquireToken(Globals.aadGraphResourceId, credential);
        //    HttpClient http = new HttpClient();
        //    string url = Globals.aadGraphEndpoint + tenant + api + "?" + Globals.aadGraphVersion;

        //    Console.ForegroundColor = ConsoleColor.Cyan;
        //    Console.WriteLine("PATCH " + url);
        //    Console.WriteLine("Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");
        //    Console.WriteLine("Content-Type: application/json");
        //    Console.WriteLine("");
        //    Console.WriteLine(json);
        //    Console.WriteLine("");

        //    HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
        //    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await http.SendAsync(request);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        string error = await response.Content.ReadAsStringAsync();
        //        object formatted = JsonConvert.DeserializeObject(error);
        //        throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
        //    }

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine((int)response.StatusCode + ": " + response.ReasonPhrase);
        //    Console.WriteLine("");

        //    return await response.Content.ReadAsStringAsync();
        //}

        //private async Task<string> SendGraphPostRequest(string api, string json)
        //{
        //    // NOTE: This client uses ADAL v2, not ADAL v4
        //    AuthenticationResult result = authContext.AcquireToken(Globals.aadGraphResourceId, credential);
        //    HttpClient http = new HttpClient();
        //    string url = Globals.aadGraphEndpoint + tenant + api + "?" + Globals.aadGraphVersion;

        //    Console.ForegroundColor = ConsoleColor.Cyan;
        //    Console.WriteLine("POST " + url);
        //    Console.WriteLine("Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");
        //    Console.WriteLine("Content-Type: application/json");
        //    Console.WriteLine("");
        //    Console.WriteLine(json);
        //    Console.WriteLine("");

        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
        //    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await http.SendAsync(request);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        string error = await response.Content.ReadAsStringAsync();
        //        object formatted = JsonConvert.DeserializeObject(error);
        //        throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
        //    }

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine((int)response.StatusCode + ": " + response.ReasonPhrase);
        //    Console.WriteLine("");

        //    return await response.Content.ReadAsStringAsync();
        //}

        #endregion

        private async Task<string> SendGraphGetRequest(string api, string query)
        {
            // First, use ADAL to acquire a token using the app's identity (the credential)
            // The first parameter is the resource we want an access_token for; in this case, the Graph API.
            AuthenticationResult result = authContext.AcquireTokenAsync(AppConfig.AzureAdOptions.GraphResourceId, credential).Result;
            
            // For B2C user managment, be sure to use the 1.6 Graph API version.
            HttpClient http = new HttpClient();

            string url = string.Format("{0}/{1}/{2}?{3}{4}{5}",
                AppConfig.AzureAdOptions.GraphResourceId.TrimEnd('/'),
                TenantId,
                api.Trim('/'),
                AppConfig.AzureAdOptions.GraphVersion,
                !string.IsNullOrEmpty(query) ? "&" : "",
                query
                );

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("GET " + url);
            Console.WriteLine("Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");
            Console.WriteLine("");

            // Append the access token for the Graph API to the Authorization header of the request, using the Bearer scheme.
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            HttpResponseMessage response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                object formatted = JsonConvert.DeserializeObject(error);
                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((int)response.StatusCode + ": " + response.ReasonPhrase);
            Console.WriteLine("");

            return await response.Content.ReadAsStringAsync();
        }

#if DEBUG
        public async Task<string> GraphTest()
        {
            try
            {
                // The ClientCredential is where you pass in your client_id and client_secret, which are 
                // provided to Azure AD in order to receive an access_token using the app's identity.
                var credential = new Microsoft.Identity.Client.ClientCredential(AppConfig.AzureAdOptions.ClientSecret);

                var cca = new Microsoft.Identity.Client.ConfidentialClientApplication(
                    AppConfig.AzureAdOptions.ClientId,
                    "https://login.microsoftonline.com/nekosoftbtgmail.onmicrosoft.com",
                    $"{AppConfig.WebApplication.BaseUrl.TrimEnd('/')}/{AppConfig.AzureAdOptions.CallbackPath.TrimStart('/')}",
                    credential,
                    null,
                    null);

                var scopes =
                    "email User.Read User.ReadBasic.All Mail.Send"
                    .Split(' ')
                    .Select(t => ("https://graph.microsoft.com/" + t).ToLower())
                    .ToArray();

                var result = cca.AcquireTokenForClientAsync(new[] { "https://graph.microsoft.com/.default" }).Result;
                //var result = cca.AcquireTokenForClientAsync(scopes).Result; does not work(?)

                var graphClient = new Microsoft.Graph.GraphServiceClient(new Microsoft.Graph.DelegateAuthenticationProvider(
                    async requestMessage =>
                    {
                        // Passing tenant ID to the sample auth provider to use as a cache key
                        //var accessToken = await _authProvider.GetUserAccessTokenAsync(userId);

                        // Append the access token to the request
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

                        // This header identifies the sample in the Microsoft Graph service. If extracting this code for your project please remove.
                        //requestMessage.Headers.Add("SampleID", "aspnetcore-connect-sample");
                    }));


                var userId = "d97257fc-15e8-4f06-8384-988e055f72ca";

                try
                {
                    // Load user profile.
                    var users = await graphClient.Users.Request().GetAsync();
                    //var user = await graphClient.Users[userId].Request().GetAsync(); does not work(?)

                    return JsonConvert.SerializeObject(users, Formatting.Indented);
                }
                catch (Microsoft.Graph.ServiceException e)
                {
                    switch (e.Error.Code)
                    {
                        case "Request_ResourceNotFound":
                        case "ResourceNotFound":
                        case "ErrorItemNotFound":
                        case "itemNotFound":
                            return JsonConvert.SerializeObject(new { Message = $"User '{userId}' was not found." }, Formatting.Indented);
                        case "ErrorInvalidUser":
                            return JsonConvert.SerializeObject(new { Message = $"The requested user '{userId}' is invalid." }, Formatting.Indented);
                        case "AuthenticationFailure":
                            return JsonConvert.SerializeObject(new { e.Error.Message }, Formatting.Indented);
                        case "TokenNotFound":
                            //await httpContext.ChallengeAsync();
                            return JsonConvert.SerializeObject(new { e.Error.Message }, Formatting.Indented);
                        default:
                            return JsonConvert.SerializeObject(new { Message = "An unknown error has occured." }, Formatting.Indented);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
#endif
    }
}
