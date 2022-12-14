using API.Extensions;
using Aplication.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddMediatR(typeof(AddBook.AddBooKCommand).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
   options.AddPolicy("MyCors", builder =>
   { 
        builder.AllowAnyHeader()
           .AllowAnyMethod()
          .AllowAnyOrigin();
   });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("MyCors");
app.MapControllers();

app.Run();