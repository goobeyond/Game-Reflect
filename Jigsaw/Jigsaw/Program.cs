using Jigsaw;
using Jigsaw.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameContext>(options =>
       options
        .UseSqlite(@"Data Source=Jigsaw.db")
       );

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "UI")),
    RequestPath = "/UI"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Startup.Run();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();
    dbContext.Games.Add(new Jigsaw.Models.Game()
    {
        Id = 1,
        CurrentState = new List<int> { 4, 3, 2, 1 },
        Answer = new List<int> { 1, 2, 3, 4 }
    });
    dbContext.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
