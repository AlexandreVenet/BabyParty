var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// CORS
string devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
	options.AddPolicy(devCorsPolicy, builder => {
		builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
	});
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors(devCorsPolicy); // CORS
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
