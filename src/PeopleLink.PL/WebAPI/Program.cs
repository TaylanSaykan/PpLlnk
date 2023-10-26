global using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PeopleLinkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PeopleLinkContext")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    ));

builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<ILeaveDtoService, LeaveDtoService>();
builder.Services.AddScoped<IExpenseDtoService, ExpenseDtoService>();
builder.Services.AddScoped<IAdvanceDtoService, AdvanceDtoService>();
builder.Services.AddScoped<IAdvanceService, AdvanceService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyDtoService, CompanyDtoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var peopleLinkContext = scope.ServiceProvider.GetRequiredService<PeopleLinkContext>();
    await PeopleLinkContextSeed.SeedAsync(peopleLinkContext);
}

app.Run();
