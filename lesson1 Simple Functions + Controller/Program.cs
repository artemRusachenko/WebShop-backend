global using lesson1_Simple_Functions___Controller.Models;
global using Microsoft.EntityFrameworkCore;
global using lesson1_Simple_Functions___Controller.DTOs.PoductsDtos;
global using lesson1_Simple_Functions___Controller.DTOs.UsersDtos;
global using lesson1_Simple_Functions___Controller.Responses;
using lesson1_Simple_Functions___Controller.Services.ProductService;
using lesson1_Simple_Functions___Controller.Services.UserService;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.Services.OrderService;
using lesson1_Simple_Functions___Controller.Services.CategoryService;
using lesson1_Simple_Functions___Controller.Services.CustomerService;
using lesson1_Simple_Functions___Controller.Services.AuthService;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using lesson1_Simple_Functions___Controller.Services.ReviewService;
using lesson1_Simple_Functions___Controller.Services.BrandService;
using lesson1_Simple_Functions___Controller.Services.ColorController;

var myCors = "myCors";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myCors", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//builder.Services.AddDbContext<SqlServerContext>();
builder.Services.AddDbContext<PostgreSqlContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IColorService, ColorService>();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Token").Value!))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
