// Ignore Spelling: Reddit

using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Models;
using Reddit.Integration.Library.Services;
using Reddit.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Tests.Library.Tests {
    [TestCaseOrderer(
    ordererTypeName: "Reddit.Integration.Tests.Utils.PriorityOrderer",
    ordererAssemblyName: "Reddit.Integration.Tests")]
    public class RedditUserServiceTests {

        private RedditServiceConfig? _redditServiceConfig;


        public RedditUserServiceTests() {

            // Get values from the config given their key and their target type.
            _redditServiceConfig = RedditConfigurationTests.GetConfiguration();

        }


        [Fact, TestPriority(9)]
        public async void GetTopUsersAsync_Invalid_RefreshToken() {

            if (_redditServiceConfig != null) {

                RedditUserService rus = new RedditUserService(_redditServiceConfig);

                await Assert.ThrowsAsync<RedditException>(async () => await rus.GetTopUsersAsync("", TokenType.RefreshToken));
            }
            else {
                Assert.Fail("Could not find the service config");
            }

        }

        [Fact, TestPriority(10)]
        public async void GetTopUsersAsync_Valid_RefreshToken() {

            if (_redditServiceConfig != null && !string.IsNullOrWhiteSpace(RedditTokenServiceTests.RefreshToken)) {

                RedditUserService rus = new RedditUserService(_redditServiceConfig);

                var result = await rus.GetTopUsersAsync(RedditTokenServiceTests.RefreshToken, TokenType.RefreshToken);

                Assert.NotNull(result);
            }
            else {
                Assert.Fail("Could not find the service config");
            }

        }
        [Fact, TestPriority(11)]
        public async void GetTopUsersAsync_NoOfUsers() {

            if (_redditServiceConfig != null && _redditServiceConfig.redditConfig != null && !string.IsNullOrWhiteSpace(RedditTokenServiceTests.RefreshToken)) {

                //Set Max Users to 3
                _redditServiceConfig.redditConfig.MaxUsers = 3;

                RedditUserService rus = new RedditUserService(_redditServiceConfig);

                var result = await rus.GetTopUsersAsync(RedditTokenServiceTests.RefreshToken, TokenType.RefreshToken);

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
