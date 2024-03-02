using Microsoft.EntityFrameworkCore;
using ProjectManagementTool.Data;

namespace ProjectManagementTool
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // DbContext registrieren
            services.AddDbContext<ProjectOrganizerDbContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ProjectOrganizerDbContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}
