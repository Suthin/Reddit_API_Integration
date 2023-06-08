// Ignore Spelling: REDDIT SUBREDDIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Configuration {
    public class GeneralConstants {

        public const string REDDIT_CONFIG_SECTION_NAME = "Authentication";
        public const string REDDIT_CONFIG_CONNECTION_STRING = "DefaultConnection";
        public const string REDDIT_CONFIG_SECTION_HEADER_PATH = "Authentication:RedditConfig";
        public const string REDDIT_CONFIG_SECTION_CLIENT_ID = "ClientId";
        public const string REDDIT_CONFIG_SECTION_CLIENT_SECRET = "ClientSecret";
        
        public const string REDDIT_SCOPE_READ = "read";
        public const string REDDIT_SCOPE_HISTORY = "history";

        public const string REDDIT_ACCESS_TOKEN_NAME = "access_token";

        public const string REDDIT_SUBREDDIT_USER_POPULAR = "popular";

        public const int DEFAULT_POSTS_MAX_COUNT = 10;
        public const int DEFAULT_USERS_MAX_COUNT = 10;

    }
}
