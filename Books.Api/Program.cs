using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Application.Mapping;
using Books.Application.Query.Country;
using Books.Application.Services;
using Books.Infrastructure.Command.Country;
using Books.Infrastructure.Configuration;
using Books.Infrastructure.Data;
using Books.Infrastructure.Helpers;
using Books.Infrastructure.Repositories;
using Books.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

namespace Books.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var jwtSettings = configuration
            .GetSection("Jwt")
            .Get<JwtSettings>()
            ?? throw new Exception("JWT settings not configured.");

            builder.Services.Configure<JwtSettings>(
                configuration.GetSection("Jwt"));
            builder.Services.Configure<TimeToExpireCache>(
                configuration.GetSection("TimeToExpire"));
            //      builder.Services.AddDbContext<LibraryDbContext>(options =>
            //options.UseMySql(
            //    configuration.GetConnectionString("ConnectionToMySql"),
            //    ServerVersion.AutoDetect(
            //        configuration.GetConnectionString("ConnectionToMySql")
            //    )));
            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCountryCommand).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCountryByIdQuery).Assembly));
            //builder.Services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //});
            builder.Services.AddMemoryCache();

            builder.Services.AddAutoMapper(
                _ => { }, //пустий конфігураційний делегат.
                typeof(BookProfile).Assembly,
                typeof(UserProfile).Assembly,
                typeof(GenreProfile).Assembly,
                typeof(AuthorProfile).Assembly
                );
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });



            // Add services to the container.
            builder.Services.AddScoped<IBookRepository,BookRepository>();
            builder.Services.AddScoped<IBookService, BookSerice>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IHashHelper, HashHelper>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            //builder.Services.AddScoped<ICacheService, MemoryCacheService>();
            builder.Services.AddScoped<ICacheService, RedisCachingService>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var config = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(config);
            })


            ;
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Key)
                ),

                ClockSkew = TimeSpan.Zero
            };
        });

            builder.Services.AddAuthorization();


            var app = builder.Build();
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
