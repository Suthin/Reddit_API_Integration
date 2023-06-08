// Ignore Spelling: Reddit

using Reddit.Integration.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Configuration {
    public class RedditServiceConfig {

        public static readonly string SECTION_NAME = GeneralConstants.REDDIT_CONFIG_SECTION_NAME;

        public RedditConfig? redditConfig { get; set; }
        public class RedditConfig {
            public string? ClientId { get; set; }
            public string? ClientSecret { get; set; }          

            public string? SubReddit { get; set; }

            public int MaxPosts { get; set; }

            public int MaxUsers { get; set; }

            public int Port { get; set; }

            public string? ChromeBrowserPath { get; set; }
        }
    }    
}
