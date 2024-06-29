using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Custom configurations
builder.Services.ConfigureCors();
builder.Services.ConfigureSqlServer(builder.Configuration);
builder.ConfigureJwtOptions();
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.GeneralConfiguration(builder.Configuration);
builder.Services.ConfigureAutomapper();
builder.Services.ConfigureRepositoryManager();
builder.Services.AddIdentityService(builder.Configuration);

builder.Services.ConfigureServiceManager();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.ConfigureBackGroundService();
builder.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
