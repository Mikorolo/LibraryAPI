using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LibraryAPI.Repositories;

namespace LibraryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddCors();
            services.AddDbContext<BookContext>(o => o.UseSqlite("Data source=books.db"));
            services.AddDbContext<MoviesContext>(o => o.UseSqlite("Data source=movies.db"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryAPI", Version = "v1" });
            });
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryAPI v1"));
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
