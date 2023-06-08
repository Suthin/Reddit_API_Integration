// Ignore Spelling: Reddit

using Microsoft.Extensions.Configuration;
using Reddit.Integration.Library.Configuration;
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
    public class RedditConfigurationTests {

        private RedditServiceConfig? _redditServiceConfig;

        public RedditConfigurationTests() {

            // Get values from the config given their key and their target type.
            _redditServiceConfig = GetConfiguration();

        }

        [Fact, TestPriority(1)]
        public void CanGetSettings() {

            var result = _redditServiceConfig != null && _redditServiceConfig.redditConfig != null;

            Assert.True(result);

        }

        [Fact, TestPriority(2)]
        public void HasClientId() {


            var result = _redditServiceConfig != null && _redditServiceConfig.redditConfig != null && !string.IsNullOrWhiteSpace(_redditServiceConfig.redditConfig.ClientId);

            Assert.True(result);

        }
        [Fact, TestPriority(3)]
        public void HasClientSecret() {


            var result = _redditServiceConfig != null && _redditServiceConfig.redditConfig != null && !string.IsNullOrWhiteSpace(_redditServiceConfig.redditConfig.ClientSecret);

            Assert.True(result);

        }

        [Fact, TestPriority(4)]
        public void HasSubReddit() {


            var result = _redditServiceConfig != null && _redditServiceConfig.redditConfig != null && !string.IsNullOrWhiteSpace(_redditServiceConfig.redditConfig.SubReddit);

            Assert.True(result);

        }
        [Fact, TestPriority(5)]
        public void HasBrowserPath() {


            var result = _redditServiceConfig != null && _redditServiceConfig.redditConfig != null && !string.IsNullOrWhiteSpace(_redditServiceConfig.redditConfig.ChromeBrowserPath);

            Assert.True(result);

        }


        #region Public Methods

        public static RedditServiceConfig? GetConfiguration() {

            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            return config.GetRequiredSection(RedditServiceConfig.SECTION_NAME).Get<RedditServiceConfig>();
        }

        #endregion
    }
}
