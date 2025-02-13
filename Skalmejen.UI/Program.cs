using Skalmejen.UI;
using Skalmejen.UI.Pages;

var builder = WebApplication
    .CreateBuilder(args)
    .AddConfiguration()
    .AddServices();

// Add services to the container.

var app = builder.Build();
app.UseServicePipeline();

app.Run();
