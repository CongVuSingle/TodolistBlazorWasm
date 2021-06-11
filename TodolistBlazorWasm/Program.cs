using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TodolistBlazorWasm.Services;

namespace TodolistBlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredToast();
            builder.Services.AddTransient<ITaskAPIClient, TaskAPIClient>();
            builder.Services.AddTransient<IUserAPIClient, UserAPIClient>();
            builder.Services.AddBlazoredLocalStorage();

            // để dùng được ApiAuthenticationStateProvider thì cài package Microsoft.AspNetCore.Components.Authorization
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            builder.Services.AddScoped(sp => new HttpClient 
            { 
                BaseAddress = new Uri(builder.Configuration["BackendApiUrl"])   
            });

            await builder.Build().RunAsync();
        }
    }
}
