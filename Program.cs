using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheNest;
using Components.ApiService;
using BlazorPanzoom;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazorPanzoomServices();
builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<Constants>();
builder.Services.AddScoped<ApiService>();


await builder.Build().RunAsync();
