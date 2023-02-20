using Business;
using Business.IRepositorios;
using Data;
using Data.Repositories;




namespace GestorEscolar_API
{
    public  class Startup
    {
        public IConfiguration Configuration { get;}
        public Startup(IConfiguration configuration)
        {
            Configuration= configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded= context => true;
            //    options.MinimumSameSitePolicy= SameSiteMode.None;
            //});

            string conn = Configuration.GetConnectionString("ConnectionDB");
            services.AddTransient<ICuentasRepositorio>(a => new CuentasRespositorio(conn));


            services.AddTransient<ICuentas, Cuentas>();


            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
         
            

            
        }

      
    }
}
