using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentMicroservice.Bank;
using StudentMicroservice.Data;
using StudentMicroservice.IReposirory;
using StudentMicroservice.Models;
using StudentMicroservice.Repository;
using StudentMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<Student, IdentityRole>(options =>
{
    // Password policy
    options.Password.RequiredLength = 8;  
    options.Password.RequireDigit = true;  
    options.Password.RequireNonAlphanumeric = true; 
    options.Password.RequireUppercase = true;  
    options.Password.RequireLowercase = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = true; // Enable phone number confirmation
    options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultEmailProvider;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ILgasRepository, LgasRepository>();
builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<IMainRepository, MainRepository>();
builder.Services.Configure<BankApiOptions>(builder.Configuration.GetSection("BankApi"));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
