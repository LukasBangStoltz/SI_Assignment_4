using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UserService.Context;
using UserService.IdentityConfig;
using UserService.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddHttpContextAccessor();


builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources(configuration))
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddInMemoryClients(IdentityConfiguration.Clients(configuration))
    .AddProfileService<ProfileService>()
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

builder.Services.AddDbContext<DbApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

var identityServer = configuration["UserService:Host"];

//// Authentication
builder.Services.AddAuthentication("token")
    .AddJwtBearer("token", options =>
    {
        options.Authority = identityServer;
        options.TokenValidationParameters.ValidateAudience = true;
        options.Audience = "UserService";
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
        options.RequireHttpsMetadata = false;
    });

string output = "";
foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
{
    if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet && item.OperationalStatus == OperationalStatus.Up)
    {
        foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
        {
            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
            {
                output = ip.Address.ToString();
            }
        }
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials());


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();

app.Run();
