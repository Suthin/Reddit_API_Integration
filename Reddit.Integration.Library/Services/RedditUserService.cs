using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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
    public class RedditUserService : IRedditUserService {


        private readonly RedditServiceConfig _redditServiceConfig;

        private readonly ILogger<RedditUserService> _logger = LogManager.CreateLogger<RedditUserService>();


        public RedditUserService(RedditServiceConfig redditServiceConfig) {
            this._redditServiceConfig = redditServiceConfig;
        }


        public Task<IEnumerable<User>> GetTopUsersAsync(string token, TokenType tokenType, string subRedditName = "", int noOfUsers = 0) {

            return Task.Run<IEnumerable<User>>(() => GetTopUsers(token, tokenType, subRedditName, noOfUsers));
        }

        private List<User> GetTopUsers(string token, TokenType tokenType, string subRedditName = "", int noOfUsers = 0) {

            try {

                //Validate Access Token
                if (string.IsNullOrWhiteSpace(token)) {
                    throw new RedditException("Invalid Parameter/Configuration: Token.");
                }


                if (this._redditServiceConfig == null || this._redditServiceConfig.redditConfig == null) {

                    throw new RedditException("Invalid Parameter/Configuration:Reedit Configuration.");

                }

                //Check for SubReddit Name if its not passed we can read it from configuration file
                if (string.IsNullOrWhiteSpace(subRedditName)) {

                    if (string.IsNullOrEmpty(this._redditServiceConfig.redditConfig.SubReddit)) {

                        throw new RedditException("Invalid Parameter/Configuration:SubReddit.");
                    }
                    else {

                        //We can set the subreddit from config file
                        subRedditName = this._redditServiceConfig.redditConfig.SubReddit;
                    }

                }

                //Validate APP Id

                if (string.IsNullOrEmpty(this._redditServiceConfig.redditConfig.ClientId)) {

                    throw new RedditException("Invalid Parameter/Configuration:ClientId.");
                }

                //Validate APP Secret

                if (string.IsNullOrEmpty(this._redditServiceConfig.redditConfig.ClientSecret)) {

                    throw new RedditException("Invalid Parameter/Configuration:Client Secret.");
                }

                if (noOfUsers < 1) {
                    //Validate the no of posts and set accordingly
                    noOfUsers = this._redditServiceConfig.redditConfig.MaxUsers > 1 ? this._redditServiceConfig.redditConfig.MaxUsers : GeneralConstants.DEFAULT_USERS_MAX_COUNT;
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

                var users = reddit.GetUserSubreddits(GeneralConstants.REDDIT_SUBREDDIT_USER_POPULAR, limit: noOfUsers);


                List<Models.User> userList = new List<User>();

                if (users != null && users.Count > 0) {

                    users.ForEach(p => userList.Add(new User { FullName = p.Fullname, Title = p.Title, userName = p.Name, Url = p.URL }));

                }

                return userList;
            }
            catch (RedditException ex) {
                throw ex;
            }
            catch (Exception ec) {

                //Now we can log the exception

                _logger.LogError(ec, ec.Message);

                //Since we already logged the error information we can return custom exception back
                throw new RedditException("Unknown error in GetTopUsers.");

            }
        }
    }
}
