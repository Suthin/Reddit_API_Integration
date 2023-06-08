using Microsoft.Extensions.Configuration;
using Reddit;
using Reddit.AuthTokenRetriever;
using Reddit.Inputs;
using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Models;
using Reddit.Integration.Library.Services;
using Reddit.Models;
using System.Diagnostics;
using System.IO;


try {

    // Build a config object, using env vars and JSON providers.
    IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

    // Get values from the config given their key and their target type.
    var settings = config.GetRequiredSection(RedditServiceConfig.SECTION_NAME).Get<RedditServiceConfig>();


    //Validate configuration
    if (settings == null || settings.redditConfig == null) {
        throw new RedditException("Invalid Parameter/Configuration:Reedit Configuration.");
    }

    //validate ClientId
    if (string.IsNullOrEmpty(settings.redditConfig.ClientId)) {

        throw new RedditException("Invalid Parameter/Configuration:Client Id.");
    }

    //Validate Client Secret
    if (string.IsNullOrEmpty(settings.redditConfig.ClientSecret)) {

        throw new RedditException("Invalid Parameter/Configuration:Client Secret.");
    }
    if (string.IsNullOrEmpty(settings.redditConfig.ChromeBrowserPath)) {

        throw new RedditException("Invalid Parameter/Configuration:Chrome browser path.");
    }
    else {

        if (!File.Exists(settings.redditConfig.ChromeBrowserPath)) {
            throw new RedditException("Invalid Parameter/Configuration:Chrome browser path.");
        }

    }

    //Get the token.
    string refreshToken = AuthorizeUser(settings.redditConfig.ClientId, settings.redditConfig.ClientSecret, settings.redditConfig.ChromeBrowserPath);


    //Now we can get the 

    var reddit = new RedditClient(settings.redditConfig.ClientId, refreshToken: refreshToken, appSecret: settings.redditConfig.ClientSecret);


    RedditPostService rps = new RedditPostService(settings);
    var resPosts = await rps.GetTopPostsAsync(refreshToken, TokenType.RefreshToken);


    string pageContent = Environment.NewLine + "######## Top 10 Posts ########" + Environment.NewLine;

    if (resPosts != null && resPosts.Any()) {
        foreach (Post post in resPosts) {

            pageContent += Environment.NewLine + "### [" + post.Title + "](" + post.Url + ") Up vote Received:" + post.UpVotes + "  Score:" + post.Score + Environment.NewLine;

        }
    }
    else {
        pageContent += "*There were no new top posts today.*";
    }

    Console.WriteLine(pageContent);



    RedditUserService rus = new RedditUserService(settings);
    var resUsers = await rus.GetTopUsersAsync(refreshToken, TokenType.RefreshToken);




    pageContent = Environment.NewLine + "######## Top 10 Users ########" + Environment.NewLine;

    if (resUsers != null && resUsers.Any()) {

        foreach (var user in resUsers) {

            pageContent += Environment.NewLine + "### " + user.Title + "(" + user.userName + " / " + user.FullName + ")  Profile Url:" + user.Url + Environment.NewLine;
        }


    }



    Console.WriteLine(pageContent);

}
catch (RedditException ex) {

    Console.WriteLine(ex.Message);
}
catch (Exception) {


    Console.WriteLine("Unknown Error!!.");

}

static string AuthorizeUser(string appId, string appSecret, string browserPath, int port = 8080) {
    // Create a new instance of the auth token retrieval library.  --Kris
    AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, port, "localhost", null, appSecret);

    // Start the callback listener.  --Kris
    // Note - Ignore the logging exception message if you see it.  You can use Console.Clear() after this call to get rid of it if you're running a console app.
    authTokenRetrieverLib.AwaitCallback();

    // Open the browser to the Reddit authentication page.  Once the user clicks "accept", Reddit will redirect the browser to localhost:8080, where AwaitCallback will take over.  --Kris
    OpenBrowser(authTokenRetrieverLib.AuthURL(), browserPath);

    // Replace this with whatever you want the app to do while it waits for the user to load the auth page and click Accept.  --Kris

    do {

        Console.WriteLine("Did you receive the token in browser? (Enter 'Q' for terminating the program).");

        var keyInfo = Console.ReadKey();

        if (keyInfo.KeyChar == 'y' || keyInfo.KeyChar == 'Y') {
            break;
        }
        else if (keyInfo.KeyChar == 'q' || keyInfo.KeyChar == 'Q') {
            Console.WriteLine("Terminating...");
            Environment.Exit(0);
        }

    }
    while (true);

    // Cleanup.  --Kris
    authTokenRetrieverLib.StopListening();

    return authTokenRetrieverLib.RefreshToken;
}
static void OpenBrowser(string authUrl, string browserPath = @"C:\Program Files\Google\Chrome\Application\chrome.exe") {
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