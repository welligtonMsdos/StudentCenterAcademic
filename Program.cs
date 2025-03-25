using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

            builder.Services.AddMudServices();            

            builder.Services.AddTransient<IStudentCenterService, StudentCenterService>();

            builder.Services.AddHttpClient("StudentCenterAcademicAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:StudentCenterAPI"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            await builder.Build().RunAsync();
        }
    }
}
