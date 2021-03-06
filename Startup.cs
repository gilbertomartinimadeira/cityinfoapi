﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Entities;
using CityInfo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using AutoMapper;
using CityInfo.Models;

namespace CityInfo
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
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddMvcOptions(options => {
                        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    });

            services.AddTransient<IMailService,LocalMailService>();

            services.AddSingleton(Configuration);

            var SqliteConnectionString = Configuration["SQLITECONNECTION"];

            services.AddDbContext<CityInfoContext>(options => {            
                options.UseSqlite( SqliteConnectionString);
            });

            services.AddScoped<ICidadeRepository, CidadeRepository>();


            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            AutoMapper.Mapper.Initialize(mappingConfig => {

                mappingConfig.CreateMap<Cidade, CidadeSemPontosturisticosDTO>();
                mappingConfig.CreateMap<Cidade, CidadeDTO>();
                mappingConfig.CreateMap<PontoTuristico, PontoTuristicoDTO>();
                mappingConfig.CreateMap<PontoTuristicoDTO, PontoTuristico>();
                mappingConfig.CreateMap<PontoTuristicoParaUpdateDTO, PontoTuristico>();
                mappingConfig.CreateMap<PontoTuristicoNovoDTO, PontoTuristico>();


            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
