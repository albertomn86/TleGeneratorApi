using TlegeneratorApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IAppDbContext, AppDbContext>();
builder.Services.AddControllers();

var app = builder.Build();

app.Run();
