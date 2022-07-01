using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// OPTIONS
builder.Services.Configure<Context>(builder.Configuration.GetSection(Context.CONNECTION_STRING));

// REPOSITORIES
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();

// SERVICES
builder.Services.AddScoped<IPersonService, PersonService>();


var app = builder.Build();

app.Urls.Add("https://localhost:5004");
app.Urls.Add("http://localhost:5005");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();