
using BAL.Database.DatabaseContext;
using BAL.Database.DatabaseIdentity;
using BAL.Seed;
using BAL.UnitOfWork;
using BLL.ControllerSide.Admin;
using BLL.ControllerSide.Login;
using BLL.ControllerSide.Manager;
using BLL.ControllerSide.Personnel;
using BLL.ControllerSide.SuperAdmin;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace HR_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // Handle circular references
                    options.SerializerSettings.Formatting = Formatting.Indented; // Optional: for pretty print
                });

            builder.Services.AddDbContext<Database_Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connStr"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<RoleSeeder>();

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<Database_Context>()
                .AddDefaultTokenProviders();

            // Register other services
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<MailSenderService>();
            builder.Services.AddScoped<RoleService>();

            builder.Services.AddScoped<SuperAdmin>();

            builder.Services.AddScoped<ManagerUpdate>();
            builder.Services.AddScoped<ManagerRead>();

            builder.Services.AddScoped<PersonnelCreate>();
            builder.Services.AddScoped<PersonnelRead>();
            builder.Services.AddScoped<PersonnelUpdate>();
            builder.Services.AddScoped<PersonnelDelete>();

            builder.Services.AddScoped<Login>();

            builder.Services.AddScoped<AdminCreate>();
            builder.Services.AddScoped<AdminRead>();
            builder.Services.AddScoped<AdminUpdate>();
            builder.Services.AddScoped<AdminDelete>();
            // Configure Authentication
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
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSuperAdminRole", policy => policy.RequireRole("SuperAdmin"));
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
                options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
                await roleSeeder.SeedRolesAsync(); 
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
