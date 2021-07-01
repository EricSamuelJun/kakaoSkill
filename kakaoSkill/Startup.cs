using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kakaoSkill {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #region Create Swagger Document
            //스웨거 문서정보 생성.
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1"
                    , new Microsoft.OpenApi.Models.OpenApiInfo() {
                        Title = "aspnetcore_swagger_sample",
                        Description = "swagger 사용을 위한 예제",
                        Version = "v1",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense() {
                            Name = "virtualgiraffe.tistory.com",
                            Url = new Uri("http://virtualgiraffe.tistory.com")
                        }
                    });
                //애플리케이션의 기본 경로
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                //xml 경로
                o.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            

            //app.UseSwagger(c => {
            //    c.SerializeAsV2 = true;
            //});
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("v1/swagger.json", "aspnetcore_swagger_sample");
#if DEBUG
                    //c.SwaggerEndpoint("swagger/v1/swagger.json", "aspnetcore_swagger_sample");
#else
                    //c.SwaggerEndpoint("/webapi/swagger/v1/swagger.json", "aspnetcore_swagger_sample");
#endif
                });
            } else {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();



            
        }
    }
}
