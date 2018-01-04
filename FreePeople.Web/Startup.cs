using FreePeople.Domain;
using FreePeople.Domain.Infrastructure;
using FreePeople.Infrastructure;
using FreePeople.Persistence;
using FreePeople.Persistence.Repositories;
using FreePeople.Web.Controllers;
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

			services.AddTransient<AuthenticationProvider>();

			services.AddTransient<AdministratorFactory>();
			services.AddTransient<IAdministratorRepository, AdministratorRepository>(sp => new AdministratorRepository(sp.GetService<AdministratorFactory>(), connectionString));
			services.AddTransient(sp => new AdministratorService(sp.GetService<IAdministratorRepository>(), sp.GetService<IEmailService>(), "http://localhost:5133"));


			services.AddTransient<SpeakerFactory>();
			services.AddTransient<ISpeakerRepository, SpeakerRepository>(sp => new SpeakerRepository(sp.GetService<SpeakerFactory>(), connectionString));
			services.AddTransient(sp => new SpeakerService(sp.GetService<ISpeakerRepository>(), sp.GetService<ITalkRepository>(), sp.GetService<IEmailService>(), "http://localhost:5133"));

			services.AddTransient<CityFactory>();
			services.AddTransient<ICityRepository, CityRepository>(sp => new CityRepository(sp.GetService<CityFactory>(), connectionString));
			services.AddTransient<CityService>();

			services.AddTransient<PlaceFactory>();
			services.AddTransient<IPlaceRepository, PlaceRepository>(sp => new PlaceRepository(sp.GetService<PlaceFactory>(), connectionString));
			services.AddTransient<PlaceService>();

			services.AddTransient<TalkFactory>();
			services.AddTransient<ITalkRepository, TalkRepository>(sp => new TalkRepository(sp.GetService<TalkFactory>(), connectionString));
			services.AddTransient<TalkService>();
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
