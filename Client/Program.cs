using Blazored.LocalStorage;
using BlazorState;
using Learning.Client.Services;
using MediatR;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Client {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();
            //builder.Services.AddMudBlazorSnackbar(config =>
            //{
            //    config.PositionClass = Defaults.Classes.Position.BottomLeft;

            //    config.PreventDuplicates = false;
            //    config.NewestOnTop = false;
            //    config.ShowCloseIcon = true;
            //    config.VisibleStateDuration = 10000;
            //    config.HideTransitionDuration = 500;
            //    config.ShowTransitionDuration = 500;
            //    //config.SnackbarDefaultVariant = Variant.Filled;
            //});
            //builder.Services.AddMudBlazorResizeListener();

            // TODO: this will be on GithHub public for all and on the client when running?
            SyncfusionLicenseProvider.RegisterLicense("MzcyMDg2QDMxMzgyZTM0MmUzMG1zdExMNE43amJ3bGwwM0x1dHBNVDJMM2RDc0doZHA2djYyaWpzOU1QL1U9");
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazorState((aOptions) => {
                aOptions.Assemblies =
                new Assembly[]
                {
                        typeof(Program).GetTypeInfo().Assembly,
                };
                //aOptions.UseReduxDevToolsBehavior = true;
            });
                

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore(options => {
                options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("role1"));
            });


            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            
            builder.Services.AddScoped(x => new CoverImageService());
            //builder.Services.AddMediatR();
            builder.Services.AddScoped<IVideoService, VideoService>();
            builder.Services.AddScoped<ISlideDeckService, SlideDeckService>();
            builder.Services.AddScoped<ISlideDeckProgramService, SlideDeckProgramService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserAvatarService, UserAvatarService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserAvatarLocalService, UserAvatarLocalService>();

            builder.Services.AddScoped<ICompletedSlideDeckProgramService, CompletedSlideDeckProgramService>();
            builder.Services.AddScoped<IUserAccessSlideDeckProgramService, UserAccessSlideDeckProgramService>();
            builder.Services.AddScoped<ICompletedReviewableProgramServices, CompletedReviewableProgramServices>();
            builder.Services.AddScoped<IMenuContentService, MenuContentService>();

            await builder.Build().RunAsync();
        }
    }
}
