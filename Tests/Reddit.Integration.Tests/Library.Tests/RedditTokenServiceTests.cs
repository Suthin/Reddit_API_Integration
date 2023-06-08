// Ignore Spelling: Reddit

using Microsoft.Extensions.Configuration;
using Reddit.AuthTokenRetriever;
using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Models;
using Reddit.Integration.Library.Services;
using Reddit.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Tests.Library.Tests {

    [TestCaseOrderer(
 ordererTypeName: "Reddit.Integration.Tests.Utils.PriorityOrderer",
 ordererAssemblyName: "Reddit.Integration.Tests")]
    public class RedditTokenServiceTests {


        private static readonly object tokenLock = new object();

        private RedditServiceConfig? _redditServiceConfig;
        private static string? _refreshToken;


        public static string RefreshToken {
            get {
                return GetRefreshToken();
            }
        }


        public RedditTokenServiceTests() {

            // Get values from the config given their key and their target type.
            _redditServiceConfig = RedditConfigurationTests.GetConfiguration();

        }


        [Fact, TestPriority(1)]
        public void GetRefreshToken_Valid() {

            string refreshToken = GetRefreshToken();

            Assert.True(!string.IsNullOrEmpty(refreshToken));

        }



        #region Private Methods



        private static string GetRefreshToken() {

            var redditServiceConfig = RedditConfigurationTests.GetConfiguration();

            if (string.IsNullOrWhiteSpace(_refreshToken)) {

                if (redditServiceConfig != null && redditServiceConfig.redditConfig != null) {

                    lock (tokenLock) {

                        if (!string.IsNullOrWhiteSpace(_refreshToken)) {
                            return _refreshToken;
                        }

                        _refreshToken = AuthorizeUser(redditServiceConfig?.redditConfig?.ClientId ?? string.Empty, redditServiceConfig?.redditConfig?.ClientSecret ?? string.Empty, redditServiceConfig?.redditConfig?.ChromeBrowserPath ?? string.Empty);
                    }
                }

            }

            return _refreshToken ?? string.Empty;

        }

        private static string AuthorizeUser(string appId, string appSecret, string browserPath, int port = 8080) {
            // Create a new instance of the auth token retrieval library.  --Kris
            AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, port, "localhost", null, appSecret);

            // Start the callback listener.  --Kris
            // Note - Ignore the logging exception message if you see it.  You can use Console.Clear() after this call to get rid of it if you're running a console app.
            authTokenRetrieverLib.AwaitCallback();

            // Open the browser to the Reddit authentication page.  Once the user clicks "accept", Reddit will redirect the browser to localhost:8080, where AwaitCallback will take over.  --Kris
            OpenBrowser(authTokenRetrieverLib.AuthURL(), browserPath);

            // Replace this with whatever you want the app to do while it waits for the user to load the auth page and click Accept.  --Kris



            Thread.Sleep(10000);




            // Cleanup.  --Kris
            authTokenRetrieverLib.StopListening();

            return authTokenRetrieverLib.RefreshToken;
        }
        private static void OpenBrowser(string authUrl, string browserPath = @"C:\Program Files\Google\Chrome\Application\chrome.exe") {
            try {



                ProcessStartInfo processStartInfo = new ProcessStartInfo(authUrl);
                Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception) {
                // This typically occurs if the runtime doesn't know where your browser is.  Use BrowserPath for when this happens.  --Kris
                ProcessStartInfo processStartInfo = new ProcessStartInfo(browserPath) {
                    Arguments = authUrl
                };
                Process.Start(processStartInfo);
            }
        }

        #endregion
    }
}
