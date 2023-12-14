using FrisianPortsREST_API.Error_Logger;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>     //To Remove from deployment
{
    options.AddDefaultPolicy(
        policy => 
        {
            policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder .Services.AddSwaggerGen();

//Dapper ORM will match names with underscores
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

//Configure Logging
//Single Logging Instance for plugging into DI container
builder.Services.AddSingleton<ILoggerService, ErrorLogger>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    /*app.UseSwagger();
    app.UseSwaggerUI();*/
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();      //To Remove from deployment
app.MapControllers();

app.Run();

