using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reddit.Integration.Client.Web.Controllers;
using Reddit.Integration.Client.Web.Data;
using Reddit.Integration.Library.Configuration;
using Reddit.Integration.Library.Interfaces;
using Reddit.Integration.Library.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString(GeneralConstants.REDDIT_CONFIG_CONNECTION_STRING) ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication()
   .AddReddit(options => {
       var redditAuth = builder.Configuration.GetSection(GeneralConstants.REDDIT_CONFIG_SECTION_HEADER_PATH);
       options.ClientId = redditAuth[GeneralConstants.REDDIT_CONFIG_SECTION_CLIENT_ID]??string.Empty;
       options.ClientSecret = redditAuth[GeneralConstants.REDDIT_CONFIG_SECTION_CLIENT_SECRET] ?? string.Empty;
       options.SaveTokens = true;
       options.Scope.Add(GeneralConstants.REDDIT_SCOPE_READ);
       options.Scope.Add(GeneralConstants.REDDIT_SCOPE_HISTORY);

       options.Events.OnCreatingTicket = ctx => {
           List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();

           tokens.Add(new AuthenticationToken() {
               Name = "TicketCreated",
               Value = DateTime.UtcNow.ToString()
           });

           HomeController.AccessToken = tokens.First(p => p.Name == GeneralConstants.REDDIT_ACCESS_TOKEN_NAME).Value;

           ctx.Properties.StoreTokens(tokens);

           return Task.CompletedTask;
       };
   });

builder.Services.Configure<RedditServiceConfig>(builder.Configuration.GetSection(RedditServiceConfig.SECTION_NAME));
builder.Services.AddTransient(_ => _.GetRequiredService<IOptions<RedditServiceConfig>>().Value);
builder.Services.AddScoped<IRedditPostService, RedditPostService>();
builder.Services.AddScoped<IRedditUserService, RedditUserService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=RedditStats}/{id?}");


app.MapRazorPages();

app.Run();
