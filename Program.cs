using Microsoft.EntityFrameworkCore;
using Seylan_App_backend_latest;

var builder = WebApplication.CreateBuilder(args);

// Register DBContext with the connection string
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // Allow requests from this origin
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Add Swagger for API documentation (optional but useful)
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
// Enable CORS with the defined policy
app.UseCors("AllowLocalhost");

app.Run();