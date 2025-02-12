using Skalmejen.UI;
using Skalmejen.UI.Pages;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddServices();
// Add services to the container.

var app = builder.Build();
app.ConfigureRequestPipeline();

app.Run();
