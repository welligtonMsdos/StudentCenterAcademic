using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using StudentCenterAcademic.Interfaces;
using StudentCenterAcademic.Services;
using System;

namespace StudentCenterAcademic
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices();

            //builder.Services.AddScoped<IStudentCenterService, StudentCenterService>();

            //builder.Services.AddScoped<IStudentCenterAuthService, StudentCenterAuthService>();

            //builder.Services.AddHttpClient("StudentCenterAcademicAPI", client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:StudentCenterAPI"]);
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //});

            var uri = builder.Configuration["ServiceUrls:StudentCenterAPI"];
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(nameof(uri), "The URI string cannot be null or empty.");
            }
            builder.Services.AddHttpClient<IStudentCenterService, StudentCenterService>(c =>
           c.BaseAddress = new Uri(uri));

            var uriAuth = builder.Configuration["ServiceUrls:StudentCenterAuthAPI"];

            if (string.IsNullOrEmpty(uriAuth))
            {
                throw new ArgumentNullException(nameof(uriAuth), "The URI string cannot be null or empty.");
            }

            builder.Services.AddHttpClient<IStudentCenterAuthService, StudentCenterAuthService>(c => c.BaseAddress = new Uri(uriAuth));

            //builder.Services.AddHttpClient("StudentCenterAcademicAuthAPI", client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:StudentCenterAuthAPI"]);
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //});
            //

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            await builder.Build().RunAsync();           
        }
    }
}
