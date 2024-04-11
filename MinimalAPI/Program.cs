using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalAPI.Automapper;
using MinimalAPI.EndPoints;
using MinimalAPI.Model;
using MinimalAPI.Model.Auth;
using MinimalAPI.Repository;
using MinimalAPI.Repository.Contract;
using System.Reflection.Metadata;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT bearer token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme ="oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header

            },
            new List<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddSingleton<List<Coupon>>();
builder.Services.AddSingleton<List<UserModel>>();
builder.Services.AddTransient<ICouponRepository, CouponRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICustomTokenHandler, CustomTokenHandler>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
               builder.Configuration.GetValue<string>("Auth:SecretKey")
                )),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.CouponEndPoints();
app.UserEndPoints();
app.UseHttpsRedirection();
//app.MapPost("/api/coupons", ([FromBody] Coupon coupon) =>
//{
//    if (coupon.Id != 0 || string.IsNullOrEmpty(coupon.Name))
//    {
//        return Results.BadRequest("Invalid coupon id or coupon name");
//    }

//    if (CouponStore.Coupons.Any(x => x.Name.ToLower() == coupon.Name.ToLower()))
//    {
//        return Results.BadRequest("Coupon is already exists..");
//    }

//    coupon.Id = CouponStore.Coupons.Count + 1;
//    CouponStore.Coupons.Add(coupon);

//    return Results.Ok("Coupon Added Successfully");
//});

//app.MapPost("/api/coupons", ([FromBody] Coupon coupon) =>
//{
//    if (coupon.Id != 0 || string.IsNullOrEmpty(coupon.Name))
//    {
//        return Results.BadRequest("Invalid coupon id or coupon name");
//    }

//    if (CouponStore.Coupons.Any(x => x.Name.ToLower() == coupon.Name.ToLower()))
//    {
//        return Results.BadRequest("Coupon is already exists..");
//    }

//    coupon.Id = CouponStore.Coupons.Count + 1;
//    CouponStore.Coupons.Add(coupon);

//    return Results.CreatedAtRoute("GetCoupon", new { coupon.Id }, "Coupon Added Successfully");
//}).WithName("CreateCoupon");

app.Run();