using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheNest;
using Components.ApiService;
using BlazorPanzoom;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<Constants>();
builder.Services.AddScoped<ApiService>();
builder.Services.AddBlazorPanzoomServices();


await builder.Build().RunAsync();
