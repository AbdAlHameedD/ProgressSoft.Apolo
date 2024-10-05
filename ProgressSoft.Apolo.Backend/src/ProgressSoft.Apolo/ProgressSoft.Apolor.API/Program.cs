using Microsoft.EntityFrameworkCore;
using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Infrastructure;

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
#endregion

#region AutoMapperProfiles
builder.Services.AddAutoMapper(typeof(BusinessCardMappingProfile));
#endregion

builder.Services.AddSingleton<ResultHelper>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
