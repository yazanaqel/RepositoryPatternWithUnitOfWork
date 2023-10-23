using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUnitOfWork.Core.Repository;
using RepositoryPatternWithUnitOfWork.EF;
using RepositoryPatternWithUnitOfWork.EF.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContextPool<ApplicationDbContext>
	(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
