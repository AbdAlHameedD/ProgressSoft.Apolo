using Microsoft.EntityFrameworkCore;
using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Application.Interfaces.Repositories;
using ProgressSoft.Apolo.Application.Interfaces.Services;
using ProgressSoft.Apolo.Application.Mappings;
using ProgressSoft.Apolo.Infrastructure;
using ProgressSoft.Apolo.Infrastructure.Repositories;
using ProgressSoft.Apolo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApoloDbContext>(options => {
    string? apoloConnectionString = builder.Configuration.GetConnectionString("ApoloConnectionString");

    if (apoloConnectionString is null)  {
        throw new NullReferenceException("Connection string not exist in appsettings.json file");
    } else {
        options.UseSqlServer(apoloConnectionString);
    }
});

#region Repositories Injections
builder.Services.AddScoped<IBusinessCardRepository, BusinessCardRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
#endregion

#region Services Injections
builder.Services.AddScoped<IBusinessCardService, BusinessCardService>();
builder.Services.AddScoped<IImageService, ImageService>();
#endregion

#region AutoMapperProfiles
builder.Services.AddAutoMapper(typeof(BusinessCardMappingProfile));
builder.Services.AddAutoMapper(typeof(ImageMappingProfile));
#endregion

#region Singleton Services
builder.Services.AddSingleton<ResultHelper>();
#endregion

#region Transient Services
builder.Services.AddTransient<ICommandHandler<AddBusinessCardCommand, Result<BusinessCardModel>>, AddBusinessCardCommandHandler>();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
