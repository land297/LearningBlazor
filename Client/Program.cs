using Blazored.LocalStorage;
using Learning.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Client {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudBlazorDialog();
            builder.Services.AddMudBlazorSnackbar();
            builder.Services.AddMudBlazorResizeListener();

            // TODO: this will be on GithHub public for all and on the client when running?
            SyncfusionLicenseProvider.RegisterLicense("MzcyMDg2QDMxMzgyZTM0MmUzMG1zdExMNE43amJ3bGwwM0x1dHBNVDJMM2RDc0doZHA2djYyaWpzOU1QL1U9");
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            builder.Services.AddScoped<IVideoService, VideoService>();
            builder.Services.AddScoped<ISlideDeckService, SlideDeckService>();
            builder.Services.AddScoped<ISlideDeckProgramService, SlideDeckProgramService>();
            

            await builder.Build().RunAsync();
        }
    }
}
