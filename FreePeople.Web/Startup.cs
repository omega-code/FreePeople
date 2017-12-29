using FreePeople.Domain;
using FreePeople.Infrastructure;
using FreePeople.Persistence;
using FreePeople.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreePeople.Web
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
		}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
			var connectionString = Configuration.GetConnectionString("mainStore");
			services.AddDbContext<DataContext>(
				opts => opts.UseSqlServer(connectionString)
			);


			services.AddSingleton(sp => Configuration.GetSection("Smtp").Get<SmtpConfig>());
			services.AddSingleton<IEmailService, EmailService>();
			services.AddTransient(sp => new SpeakerService(sp.GetService<ISpeakerRepository>(), sp.GetService<IEmailService>(), "http://freepeople.world/is"));
			services.AddTransient<ISpeakerRepository, SpeakerRepository>(sp => new SpeakerRepository(sp.GetService<SpeakerFactory>(), connectionString));
			services.AddTransient<SpeakerFactory>();
			services.AddTransient<CityFactory>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
