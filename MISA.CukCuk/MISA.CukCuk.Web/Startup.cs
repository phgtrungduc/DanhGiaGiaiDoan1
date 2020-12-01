using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MISA.ApplicationCore;
using MISA.ApplicationCore.interfaces;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Services;
using MISA.CukCuk.Web.Middwares;
using MISA.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers()
                //các key của object trả về ở dạng khoogn phân biệt hoa, thường. Cần thêm cái này để viết hoa chuẩn các Key
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                }); 
            //cors
            services.AddCors();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>(); 
            services.AddScoped<ICustomerService, CustomerService>();
             
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IPositionService, PositionService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();


            app.UseRouting();
            //cors
            app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader());

            app.UseAuthorization();
            //Nhận các file static từ wwwroot
            app.UseStaticFiles();

            app.UseDefaultFiles();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
