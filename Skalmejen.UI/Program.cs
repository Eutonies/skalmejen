using Skalmejen.UI;
using Skalmejen.UI.Pages;

var builder = WebApplication
    .CreateBuilder(args)
    .AddConfiguration()
    .AddServices();

var app = builder.Build();
app.ConfigureRequestPipeline();
app.Run();
