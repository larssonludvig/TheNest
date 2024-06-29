using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheFinalsRandomLoadout;
using Components.ApiService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<Constants>();

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<ApiService>();


await builder.Build().RunAsync();
