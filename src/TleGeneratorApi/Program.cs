using TleGeneratorApi;
using TleGeneratorApi.CatalogProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IAppDbContext, AppDbContext>();
builder.Services.AddHttpClient<IHttpClient, CelestrackClient>();
builder.Services.AddScoped<ITleUpdater, TleUpdater>();

builder.Services.AddControllers();

var app = builder.Build();

app.Run();
