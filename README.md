# Reddit API Integration

Introduction
This document provides a guide for integrating the Reddit API into a C# sample project. The sample project demonstrates how to authenticate with the Reddit API, make requests to retrieve data, and handle the API responses. It serves as a starting point for building your own Reddit API integration using C#.

# Table of Contents
1.	Prerequisites
2.	Authentication
3.	API Requests
4.	Error Handling
5.	Sample Project Structure
6.	Usage 
# Prerequisites
Before starting the integration, ensure that you have the following:
1.	[.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
2.	Reddit API credentials (client ID and client secret). You can obtain these by registering your application on the [Reddit website](https://www.reddit.com/prefs/apps).
# Authentication
To authenticate with the Reddit API, follow these steps:
1.	Register your application on the Reddit website and obtain the client ID and client secret.
2.	Use the client ID and client secret to request an access token from the Reddit API.
3.	Include the access token in the request headers for subsequent API calls.

In the sample project, authentication is handled using [AspNet.Security.OAuth.Reddit](https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers), [Reddit.NET](https://github.com/sirkris/Reddit.NET) and [Reddit.AuthTokenRetrieverLib](https://github.com/sirkris/Reddit.NET) libraries, which simplifies the OAuth 2.0 authentication process.
# API Requests
The sample project demonstrates how to make API requests to retrieve data from Reddit using the [Reddit.NET library](https://github.com/sirkris/Reddit.NET). It covers the following API endpoints:
1.	Retrieve the hottest posts from a specific subreddit.
2.	Retrieve the poplar users.
For each API request, the sample project provides a corresponding method that encapsulates the necessary authentication and request logic.
# Error Handling
The sample project includes error handling to handle potential errors that may occur during API requests as well as other portions of the code to provides appropriate error messages to the user.
Error handling is implemented using try-catch blocks. Application will return the Custom Exception after logging the error based on the logger configuration.
# Sample Project Structure

Reddit.Integration
  - ── Clients
    - ── Reddit.Integration.Client.Console
    - ── Reddit.Integration.Client.Web
  - ── Tests
    - ── Reddit.Integration.Tests 
  - ── Reddit.Integration.Library

- Clients: Contains two sample client projects for showing Reddit API integration results
  - Reddit.Integration.Client.Console--> Console Application
  - Reddit.Integration.Client.WeB--> MVC Application
- Tests: Contains unit test cases for Reddit.Integration.Library
- Reddit.Integration.Library: Contains actual Reddit API integration code with external libraries.
# Usage
To use the sample project with Reddit API integration:
1.	Clone the repository or download the sample project files.
2.	Open the project in an IDE or text editor.
3.	Update the appsettings.json file with your Reddit API credentials.
4.	Build and run the project.
The sample projects will execute API requests and display the retrieved data in the console and Web browser.
