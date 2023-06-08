// Ignore Spelling: reddit

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reddit.Inputs;
using Reddit.Integration.Client.Web.Models;
using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Models;
using Reddit.Integration.Library.Services;
using System.Diagnostics;

namespace Reddit.Integration.Client.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly RedditServiceConfig _redditServiceConfig;

        public static string AccessToken = string.Empty;

        public HomeController(ILogger<HomeController> logger, RedditServiceConfig redditServiceConfig) {
            _logger = logger;
            _redditServiceConfig = redditServiceConfig;
        }

        [Authorize]
        public async Task<IActionResult> RedditStats() {

            try {

                if (!string.IsNullOrWhiteSpace(AccessToken)) {

                    RedditPostService rps = new RedditPostService(_redditServiceConfig);
                    var resPosts = await rps.GetTopPostsAsync(AccessToken, TokenType.AccessToken);

                    RedditUserService rus = new RedditUserService(_redditServiceConfig);
                    var resUsers = await rus.GetTopUsersAsync(AccessToken, TokenType.AccessToken);

                    ViewData["PageContent"] = resPosts;

                    return View(resUsers);

                }
                else {
                    //Now we can redirect to logout screen              
                    return RedirectToRoute(new { action = "Logout", controller = "Account", area = "Identity" });

                }
            }
            catch (RedditException) {

                //Redirect to error screen
                return RedirectToAction("Error");
            }
            catch (Exception ex) {

                //Log the message  
                _logger.LogError(ex, ex.Message);

                //Redirect to error screen

                return RedirectToAction("Error");
            }

        }
        [Authorize]
        public async Task<IActionResult> TopPosts() {

            try {
                if (!string.IsNullOrWhiteSpace(AccessToken)) {

                    RedditPostService rps = new RedditPostService(_redditServiceConfig);
                    var resPosts = await rps.GetTopPostsAsync(AccessToken, TokenType.AccessToken);

                    return PartialView("_TopPosts", resPosts);
                }
                else {
                    //Now we can redirect to logout screen              
                    return RedirectToRoute(new { action = "Logout", controller = "Account", area = "Identity" });

                }
            }
            catch (RedditException) {

                //Redirect to error screen
                return RedirectToAction("Error");
            }
            catch (Exception ex) {

                //Log the message  
                _logger.LogError(ex, ex.Message);

                //Redirect to error screen

                return RedirectToAction("Error");
            }
        }
        [Authorize]
        public async Task<IActionResult> TopUsers() {

            try {

                if (!string.IsNullOrWhiteSpace(AccessToken)) {

                    RedditUserService rus = new RedditUserService(_redditServiceConfig);
                    var resUsers = await rus.GetTopUsersAsync(AccessToken, TokenType.AccessToken);

                    return PartialView("_TopUsers", resUsers);
                }
                else {
                    //Now we can redirect to logout screen              
                    return RedirectToRoute(new { action = "Logout", controller = "Account", area = "Identity" });

                }
            }
            catch (RedditException) {

                //Redirect to error screen
                return RedirectToAction("Error");
            }
            catch (Exception ex) {

                //Log the message  
                _logger.LogError(ex, ex.Message);

                //Redirect to error screen

                return RedirectToAction("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}