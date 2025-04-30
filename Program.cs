using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using StudentCenterAcademic.Interfaces;
using StudentCenterAcademic.Services;

namespace StudentCenterAcademic
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
            });

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

            builder.Services.AddBlazoredLocalStorage();           

            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddAuthorizationCore();           

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            builder.Services.AddScoped<StudentStateService>();

            await builder.Build().RunAsync();           
        }
    }
}
