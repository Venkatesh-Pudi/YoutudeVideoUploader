//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Services;
//using Google.Apis.Util.Store;
//using Google.Apis.YouTube.v3;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;

//namespace YouTubeUploader.Helpers
//{
//    public class YouTubeServiceHelper
//    {

//        private static readonly string[] Scopes = { "https://www.googleapis.com/auth/youtube.upload" };
//        private static readonly string ApplicationName = "YouTubeUploader";

//        private readonly IConfiguration _configuration;
//        private readonly string _apiKey;

//        public YouTubeServiceHelper(IConfiguration configuration)
//        {
//            _configuration = configuration;
//            _apiKey = _configuration["Google:ApiKey"];
//        }

//        private static string GetCredentialsPath()
//        {

//            var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "App_Data", "credentials.json");
//            return credentialsPath;
//        }

//        //public async Task<YouTubeService> GetYouTubeServiceAsync()
//        //{
//        //    UserCredential credential;
//        //    // var credentialsPath = GetCredentialsPath();
//        //    var credentialsPath = "C:\\Users\\HP\\source\\repos\\YouTubeUploader\\YouTubeUploader\\wwwroot\\App_Data\\credentials.json";
//        //    Console.WriteLine($"Credentials Path: {credentialsPath}"); 
//        //    using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
//        //    {
//        //        var clientSecrets = GoogleClientSecrets.Load(stream).Secrets;

//        //        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
//        //            clientSecrets,
//        //            Scopes,
//        //            "user",
//        //            CancellationToken.None,
//        //            new FileDataStore("YouTubeUploader.Token.Store")
//        //        );
//        //    }

//        //    return new YouTubeService(new BaseClientService.Initializer()
//        //    {
//        //        HttpClientInitializer = credential,
//        //        ApiKey = _apiKey,
//        //        ApplicationName = ApplicationName,
//        //    });
//        //}
//        public async Task<YouTubeService> GetYouTubeServiceAsync()
//        {
//            UserCredential credential;
//            var credentialsPath = "C:\\Users\\HP\\source\\repos\\YouTubeUploader\\YouTubeUploader\\wwwroot\\App_Data\\credentials.json";
//            Console.WriteLine($"Credentials Path: {credentialsPath}");
//            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
//            {
//                var clientSecrets = GoogleClientSecrets.Load(stream).Secrets;

//                // Here you can specify the redirect URI if needed
//                string redirectUri = clientSecrets.RedirectUris.FirstOrDefault();

//                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
//                    new ClientSecrets
//                    {
//                        ClientId = clientSecrets.ClientId,
//                        ClientSecret = clientSecrets.ClientSecret
//                    },
//                    Scopes,
//                    "user",
//                    CancellationToken.None,
//                    new FileDataStore("YouTubeUploader.Token.Store"),
//                    new LocalServerCodeReceiver()
//                );
//            }

//            return new YouTubeService(new BaseClientService.Initializer()
//            {
//                HttpClientInitializer = credential,
//                ApiKey = _apiKey,
//                ApplicationName = ApplicationName,
//            });
//        }


//    }
//}


using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace YouTubeUploader.Helpers
{
    public class YouTubeServiceHelper
    {
        private static readonly string[] Scopes = { "https://www.googleapis.com/auth/youtube.upload" };
        private static readonly string ApplicationName = "YouTubeVideoUploader";

        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public YouTubeServiceHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["Google:ApiKey"];
        }

        private static string GetCredentialsPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "App_Data", "credentials.json");
        }

        public async Task<YouTubeService> GetYouTubeServiceAsync()
        {
            UserCredential credential;
            var credentialsPath = "C:\\Users\\HP\\source\\repos\\YouTubeUploader\\YouTubeUploader\\wwwroot\\App_Data\\credentials.json";
            Console.WriteLine($"Credentials Path: {credentialsPath}");

            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                var clientSecrets = GoogleClientSecrets.Load(stream).Secrets;

                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = clientSecrets.ClientId,
                        ClientSecret = clientSecrets.ClientSecret,
                        //Clienturis = clientSecrets .redirecturis,
                    },
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("YouTubeUploader.Token.Store"),
                    new LocalServerCodeReceiver() // Handles the redirect URI internally
                );
            }

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApiKey = _apiKey,
                ApplicationName = ApplicationName,
            });
        }
    }
}


