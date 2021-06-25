using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DreamProperties.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        IConfiguration Configuration { get; }

        IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DreamProperties.API", Version = "v1" });
            });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddFacebook(facebook =>
            {
                facebook.AppId = Configuration["Authentication:Facebook:AppId"];
                facebook.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                facebook.SaveTokens = true;
            });
            //.AddGoogle(google =>
            //{
            //    google.ClientId = Configuration["Authentication:Google:ClientId"];
            //    google.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //    google.SaveTokens = true;
            //})
            //.AddApple(apple =>
            //{
            //    apple.ClientId = Configuration["AppleClientId"];
            //    apple.KeyId = Configuration["AppleKeyId"];
            //    apple.TeamId = Configuration["AppleTeamId"];
            //    apple.UsePrivateKey(keyId
            //        => WebHostEnvironment.ContentRootFileProvider.GetFileInfo($"AuthKey_{keyId}.p8"));
            //    apple.SaveTokens = true;
            //});
            /*
            * For Apple signin
            * If you are running the app on Azure you must add the Configuration setting
            * WEBSITE_LOAD_USER_PROFILE = 1
            * Without this setting you will get a 
            * File Not Found exception when AppleAuthenticationHandler tries 
            * to generate a certificate using your Auth_{keyId].P8 file.
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DreamProperties.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
