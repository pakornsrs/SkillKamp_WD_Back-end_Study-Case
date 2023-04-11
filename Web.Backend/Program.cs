using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SkillkampWdStudyCaseDbContext>(x => x.UseSqlServer(connectionString));

#region Set CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

#endregion

# region Service
builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductColorService, ProductColorService>();
builder.Services.AddScoped<IProductSizeService, ProductSizeService>();
builder.Services.AddScoped<IUserTokenService, UserTokenService>();
builder.Services.AddScoped<IUserCardService, UserCardService>();
builder.Services.AddScoped<IUserAddressService, UserAddressService>();
builder.Services.AddScoped<IDiscountCampeignService, DiscountCampeignService>();
builder.Services.AddScoped<IPurchaseSessionService, PurchaseSessionService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IProductRatingService, ProductRatingService>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPurchasedOrderService, PurchasedOrderService>();
builder.Services.AddScoped<IPaymentDetailService, PaymentDetailService>();
builder.Services.AddScoped<IDiscountCouponService, DiscountCouponService>();
#endregion

//var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//builder.Services.AddJwtAuthentication(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
