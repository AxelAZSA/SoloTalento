using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayneInc.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WayneInc.Bussiness.Repository;
using WayneInc.Bussiness.AuthenticationService;
using WayneInc.Bussiness.TokenService;
using WayneInc.Bussiness.Service;

namespace WayneInc
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
            services.AddControllers();
            //Declaración del contexto de base de datos
            var connectionString = Configuration.GetConnectionString("MyDb");
            services.AddDbContext<DbWContext>(options => options.UseSqlServer(connectionString));

            //Configuración de los cors
            services.AddCors(options => options.AddPolicy(name: "wayneS", policy =>
            {
        //policy.WithOrigins("https://localhost:7273", "https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }
                ));

            //Implementación de la autentificación con JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                };
            });

            //Declaración de los esquemas de servicio
            services.AddSingleton<IPasswordHasher, BcryptPassword>();
            services.AddSingleton<TokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddScoped<ProcesoCompra>();
            services.AddScoped<ProcesoInventario>();
            services.AddScoped<ICarritoRepository, CarritoRepository>();
            services.AddScoped<ICarritoItemRepository, CarritoItemRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ITiendaRepository, TiendaRepository>();
            services.AddScoped<IArticuloRepository, ArticuloRepository>();
            services.AddScoped<ISesionRepository, SesionRepository>();
            services.AddScoped<IArticuloClienteRepository, ArticuloClienteRepository>();
            services.AddScoped<IArticuloTiendaRepository, ArticuloTiendaRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(policy =>
            {
                //policy.WithOrigins("https://localhost:7273", "https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }
                );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
