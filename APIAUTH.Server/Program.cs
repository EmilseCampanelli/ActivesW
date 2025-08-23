using APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria;
using APIAUTH.Aplication.CQRS.Commands.Categoria.UpdateCategoria;
using APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto;
using APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create;
using APIAUTH.Aplication.CQRS.Commands.Producto.UpdateProducto;
using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.CQRS.Queries.Products;
using APIAUTH.Aplication.CQRS.Queries.Users;
using APIAUTH.Aplication.Filtered;
using APIAUTH.Aplication.Mapper;
using APIAUTH.Aplication.Policies;
using APIAUTH.Aplication.Services.Implementacion;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Data.Context;
using APIAUTH.Data.Filtered;
using APIAUTH.Data.Repositorios;
using APIAUTH.Data.Repository;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using APIAUTH.Infrastructure.Services;
using APIAUTH.Infrastructure.SignalR;
using CloudinaryDotNet;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

//var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
//builder.Services.AddDbContext<ActivesWContext>(dbConection => dbConection.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDbContext<ActivesWContext>(dbConnection =>
    dbConnection.UseNpgsql(connectionString));


builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<NotificationServiceInfra>();
builder.Services.AddScoped<ICompanyService, OrganizationService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCartService, ProductCartService>();
builder.Services.AddScoped<IColorThemeRepository, ColorThemeRepository>();
builder.Services.AddScoped<IColorThemeService, ColorThemeService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IGeoRepo, GeoRepo>();
builder.Services.AddScoped<INotificationService, NotificationService>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(APIAUTH.Aplication.AssemblyReference).Assembly);
});

builder.Services.AddSingleton(provider =>
{
    var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
    if (string.IsNullOrWhiteSpace(cloudinaryUrl))
        throw new Exception("La variable CLOUDINARY_URL no está configurada");

    var uri = new Uri(cloudinaryUrl);
    var apiKey = uri.UserInfo.Split(':')[0];
    var apiSecret = uri.UserInfo.Split(':')[1];
    var cloudName = uri.Host;

    var account = new CloudinaryDotNet.Account(cloudName, apiKey, apiSecret);
    return new Cloudinary(account);
});


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductoValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IListRepository<>), typeof(ListRepository<>));
builder.Services.AddScoped<IEntityFilter<Product>, ProductEntityFilter>();
builder.Services.AddScoped<IEntityFilter<User>, UserEntityFilter>();
builder.Services.AddScoped<IEntityFilter<Orden>, OrdenEntityFilter>();
builder.Services.AddScoped<IGeoImportService, GeoImportService>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod());
});

builder.Services.AddAuthorization(options =>
{
    AuthorizationPolicies.ConfigurePolicies(options);
});

builder.Services.AddSignalR();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ActivesWContext>();
    context.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ActivesWContext>();
    var cfg = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

    Console.WriteLine($"[DB] ConnString: {cfg.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")}");
    Console.WriteLine($"[DB] Provider: {context.Database.ProviderName}");
    Console.WriteLine($"[DB] Database: {context.Database.GetDbConnection().Database}");
    Console.WriteLine($"[DB] Host: {context.Database.GetDbConnection().DataSource}");

    context.Database.Migrate();

    if (cfg.GetValue<bool>("GeoImport:RunOnStartup"))
    {
        var folder = cfg["GeoImport:Folder"] ?? "Data";
        var absFolder = Path.Combine(env.ContentRootPath, folder);
        Console.WriteLine($"[GeoImport] Folder: {absFolder}");
        Console.WriteLine($"[GeoImport] Files exist? countryInfo={File.Exists(Path.Combine(absFolder, "countryInfo.txt"))}, admin1={File.Exists(Path.Combine(absFolder, "admin1CodesASCII.txt"))}, cities5000={File.Exists(Path.Combine(absFolder, "cities5000.txt"))}");

        var svc = scope.ServiceProvider.GetRequiredService<IGeoImportService>();
        await svc.ImportAsync(absFolder);
    }
}


app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();


app.UseDeveloperExceptionPage();

app.MapHub<NotificationHub>("/notificationHub");


app.MapControllers();

app.Run();
