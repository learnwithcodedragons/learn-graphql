using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuoteOfTheDay.Data;
using QuoteOfTheDay.GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.Extensions.Hosting;

namespace QuoteOfTheDay
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuoteOfTheDayDbContext>( options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<QuoteRepository>();
            services.AddScoped<QuoteOfTheDaySchema>();

            services.AddDefer();
            services.AddHttpScope();

            services.AddSingleton<QuoteOfTheDaySchema>()
                .AddSingleton<QuoteQuery>()
                .AddGraphQL()
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
                .AddGraphTypes(typeof(QuoteOfTheDaySchema));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphQL<QuoteOfTheDaySchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
