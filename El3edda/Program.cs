using El3edda.Data;
using El3edda.Data.Cart;
using El3edda.Data.PaymentSettings;
using El3edda.Data.Services;
using El3edda.Data.Services.MediaService;
using El3edda.Data.Services.MobileService;
using El3edda.Data.Services.OrderServices;
using El3edda.Data.Services.ReviewService;
using El3edda.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

//Add Db
builder.Services.AddDbContext<AppDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("DbCN"))
);

//Add Controllers Services
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IMobileService, MobileService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IReviewService, El3edda.Data.Services.ReviewService.ReviewService>();

//Shopping Cart Services
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(c => ShoppingCart.GetShoppingCart(c));

//Payment Services
//PayPal
builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));

//Stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

//Authentication & Authorization
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }
);

var a = builder.Configuration["Facebook:AppSecret"];

//Facebook Login
builder.Services
    .AddAuthentication()
    .AddFacebook(
        facebookOptions =>
        {
            facebookOptions.AppId = builder.Configuration["Facebook:AppId"];
            facebookOptions.AppSecret = builder.Configuration["Facebook:AppSecret"];
        }
    );

// Add services to the container.
builder.Services.AddControllersWithViews();

//Razor Refresh
builder.Services.AddMvc().AddRazorRuntimeCompilation();

var app = builder.Build();

//Configure Stripe API
StripeConfiguration.ApiKey = (builder.Configuration.GetSection("Stripe")["SecretKey"]);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

//Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRoles(app).Wait();

app.Run();
