using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using Reddit.Inputs;
using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Interfaces;
using Reddit.Integration.Library.Models;
using Reddit.Integration.Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Services {
    public class RedditPostService : IRedditPostService {

        private readonly RedditServiceConfig _redditServiceConfig;

        private readonly ILogger<RedditPostService> _logger = LogManager.CreateLogger<RedditPostService>();


        public RedditPostService(RedditServiceConfig redditServiceConfig) {
            this._redditServiceConfig = redditServiceConfig;

        }


        public Task<IEnumerable<Post>> GetTopPostsAsync(string token, TokenType tokenType, string subRedditName = "", int noOfPosts = 0) {

            return Task.Run<IEnumerable<Post>>(() => GetTopPosts(token, tokenType, subRedditName, noOfPosts));
        }

        private List<Post> GetTopPosts(string token, TokenType tokenType, string subRedditName = "", int noOfPosts = 0) {

            try {

                //Validate Access Token
                if (string.IsNullOrWhiteSpace(token)) {
                    throw new RedditException(MessageHelper.GetMessage(MessageType.ValdationError, "Token"));
                }


                if (this._redditServiceConfig == null || this._redditServiceConfig.redditConfig == null) {

                    throw new RedditException(MessageHelper.GetMessage(MessageType.ValdationError, "Reddit Configuration"));

                }

                //Check for SubReddit Name if its not passed we can read it from configuration file
                if (string.IsNullOrWhiteSpace(subRedditName)) {

                    if (string.IsNullOrEmpty(this._redditServiceConfig.redditConfig.SubReddit)) {

                        throw new RedditException(MessageHelper.GetMessage(MessageType.ValdationError, "Subreddit"));
                    }
                    else {

                        //We can set the subreddit from config file
                        subRedditName = this._redditServiceConfig.redditConfig.SubReddit;
                    }

                }

                //Validate APP Id

                if (string.IsNullOrEmpty(this._redditServiceConfig.redditConfig.ClientId)) {

                    throw new RedditException(MessageHelper.GetMessage(MessageType.ValdationError, "ClientId"));
                }

                //Validate APP Secret

                if (string.IsNullOrEmpty(this._redditServiceConfig.redditConfig.ClientSecret)) {

                    throw new RedditException(MessageHelper.GetMessage(MessageType.ValdationError, "Client Secret"));
                }

                if (noOfPosts < 1) {
                    //Validate the no of posts and set accordingly
                    noOfPosts = this._redditServiceConfig.redditConfig.MaxPosts > 1 ? this._redditServiceConfig.redditConfig.MaxPosts : GeneralConstants.DEFAULT_POSTS_MAX_COUNT;
                }


                RedditClient reddit;

                if (tokenType == TokenType.AccessToken) {

                    reddit = new RedditClient(this._redditServiceConfig.redditConfig.ClientId, appSecret: this._redditServiceConfig.redditConfig.ClientSecret, accessToken: token);
                }
                else {

                    reddit = new RedditClient(this._redditServiceConfig.redditConfig.ClientId, refreshToken: token, appSecret: this._redditServiceConfig.redditConfig.ClientSecret);
                }


                var subreddit = reddit.Subreddit(subRedditName);

                var today = DateTime.Today;

                List<Models.Post> postList = new List<Post>();

                // Get the top 10 posts from the last 24 hours.  --Kris
                var posts = subreddit.Posts.GetTop(new TimedCatSrListingInput(t: GeneralConstants.REDDIT_SUBREDDIT_FILTER_TYPE_DAY, limit: noOfPosts));

                if (posts != null && posts.Count > 0) {

                    posts.ForEach(p => postList.Add(new Post { Score = p.Score, Title = p.Title, UpVotes = p.UpVotes, Url = p.Permalink }));

                }


                return postList;
            }
            catch (RedditException ex) {
                throw ex;
            }
            catch (Exception ec) {

                //Now we can log the exception

                _logger.LogError(ec, ec.Message);

                //Since we already logged the error information we can return custom exception back
                throw new RedditException(MessageHelper.GetMessage(MessageType.ValdationError, "GetTopPosts"));

            }
        }
    }
}
