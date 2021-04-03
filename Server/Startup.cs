using Learning.Server.DbContext;
using Learning.Server.Repositories;
using Learning.Server.Repositories.Base;
using Learning.Server.Service;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;

namespace Learning.Server {
    public class Startup {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            if (_env.IsProduction()) {

                services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Configuration.GetSection("AppSettings:SqlServerConnectionString").Value));
            } else {
                services.AddDbContext<AppDbContext>(x => x.UseSqlite(Configuration.GetConnectionString("Default")));
            }
        
            
            services.AddControllersWithViews();
            services.AddRazorPages();

            var t = Configuration.GetSection("AppSettings:Token").Value;
            //TODO: Check token event https://forums.asp.net/t/2138711.aspx?Invalidate+JWT+token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options => {
                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            services.AddScoped<IAzureRepo, AzureRepo>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISlideDeckRepo, SlideDeckRepo>();
            services.AddScoped<ISlideDeckProgramRepo, SlideDeckProgramRepo>();
            services.AddScoped<IVideoRepo, VideoRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserAvatarRepo, UserAvatarRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompletedSlideDeckProgramRepo, CompletedSlideDeckProgramRepo>();
            services.AddScoped<IUserAccessSlideDeckProgramRepo, UserAccessSlideDeckProgramRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IRepoBase3<BlogPost>, BlogRepo > ();
            services.AddScoped<IForgottenPasswordRepo, ForgottenPasswordRepo>();
            services.AddTransient<IMailKitEmailSender, MailKitEmailSender>();
            services.Configure<MailKitEmailSenderOptions>(options =>
            {
                options.Host_Address = Configuration.GetSection("AppSettings:SMTPserver").Value;
                options.Host_Port = 587;
                options.Host_Username = Configuration.GetSection("AppSettings:Login").Value;
                options.Host_Password = Configuration.GetSection("AppSettings:Password").Value;
                options.Sender_EMail = "noreply@mydomain.com";
                options.Sender_Name = "My Sender Name";
            });


            //services.AddCors(policy =>
            //{
            //    policy.AddPolicy("CorsPolicy", opt => opt
            //    .AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod());
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            //app.UseCors("CorsPolicy");


            app.UseRouting();
            // Added
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

           
        }
    }
}
