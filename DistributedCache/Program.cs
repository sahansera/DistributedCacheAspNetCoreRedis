using DistributedCache.Infrastructure;
using DistributedCache.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")
        ?? throw new InvalidOperationException("Connection string 'Redis' is not configured.");
    options.InstanceName = "DistributedCacheSample:";
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserService, CachedUserService>();
builder.Services.AddScoped<ICacheUserService, CacheUserService>();
builder.Services.AddScoped<ICacheProvider, CacheProvider>();
builder.Services.AddScoped<IUsersApiClient, UsersApiClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
