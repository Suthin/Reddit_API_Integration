// Ignore Spelling: Reddit

using Microsoft.Extensions.Configuration;
using Reddit.AuthTokenRetriever;
using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Models;
using Reddit.Integration.Library.Services;
using Reddit.Integration.Tests;
using Reddit.Integration.Tests.Library.Tests;
using Reddit.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Tests {



    [TestCaseOrderer(
    ordererTypeName: "Reddit.Integration.Tests.Utils.PriorityOrderer",
    ordererAssemblyName: "Reddit.Integration.Tests")]
    public class RedditPostServiceTests {


        private RedditServiceConfig? _redditServiceConfig;       


        public RedditPostServiceTests() {

            // Get values from the config given their key and their target type.
            _redditServiceConfig = RedditConfigurationTests.GetConfiguration();

        }

       
        [Fact, TestPriority(6)]
        public async void GetTopPostsAsync_Invalid_RefreshToken() {

            if (_redditServiceConfig != null) {

                RedditPostService rps = new RedditPostService(_redditServiceConfig);

                await Assert.ThrowsAsync<RedditException>(async () => await rps.GetTopPostsAsync("", TokenType.RefreshToken));
            }
            else {
                Assert.Fail("Could not find the service config");
            }

        }

        [Fact, TestPriority(7)]
        public async void GetTopPostsAsync_Valid_RefreshToken() {

            if (_redditServiceConfig != null && !string.IsNullOrWhiteSpace(RedditTokenServiceTests.RefreshToken)) {

                RedditPostService rps = new RedditPostService(_redditServiceConfig);

                var result = await rps.GetTopPostsAsync(RedditTokenServiceTests.RefreshToken, TokenType.RefreshToken);

                Assert.NotNull(result);
            }
            else {
                Assert.Fail("Could not find the service config");
            }

        }
        [Fact, TestPriority(8)]
        public async void GetTopPostsAsync_NoOfPosts() {

            if (_redditServiceConfig != null && _redditServiceConfig.redditConfig != null && !string.IsNullOrWhiteSpace(RedditTokenServiceTests.RefreshToken)) {

                //Set Max Posts to 3
                _redditServiceConfig.redditConfig.MaxPosts = 3;

                RedditPostService rps = new RedditPostService(_redditServiceConfig);

                var result = await rps.GetTopPostsAsync(RedditTokenServiceTests.RefreshToken, TokenType.RefreshToken);

                Assert.Equal(3, result.Count());
            }
            else {
                Assert.Fail("Could not find the service config/Refresh token.");
            }

        }



        #region Private Methods
        

        #endregion
    }
}
