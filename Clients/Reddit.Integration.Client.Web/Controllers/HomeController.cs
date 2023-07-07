// Ignore Spelling: reddit

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reddit.Inputs;
using Reddit.Integration.Client.Web.Models;
using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Interfaces;
using Reddit.Integration.Library.Models;
using Reddit.Integration.Library.Services;
using System.Diagnostics;

namespace Reddit.Integration.Client.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IRedditPostService _redditPostService;
        private readonly IRedditUserService _redditUserService;

        public static string AccessToken = string.Empty;

        public HomeController(ILogger<HomeController> logger, IRedditPostService redditPostService, IRedditUserService redditUserService) {
            _logger = logger;            
            _redditPostService = redditPostService;
            _redditUserService=redditUserService;
        }

        [Authorize]
        public async Task<IActionResult> RedditStats() {

            try {

                if (!string.IsNullOrWhiteSpace(AccessToken)) {

                    var resPosts = await _redditPostService.GetTopPostsAsync(AccessToken, TokenType.AccessToken);

                    var resUsers = await _redditUserService.GetTopUsersAsync(AccessToken, TokenType.AccessToken);

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
                    
                    var resPosts = await _redditPostService.GetTopPostsAsync(AccessToken, TokenType.AccessToken);

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

                    var resUsers = await _redditUserService.GetTopUsersAsync(AccessToken, TokenType.AccessToken);

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