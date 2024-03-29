﻿using Coherent.McsResultHost.McsResultApi.ConfigurationExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi {
  public class WebApiStartup {
    public WebApiStartup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services
        .AddMvcCore()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
        .ConfigureJson();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      app.ConfigureGlobalExceptionHandler();
      app.UseMvc();
    }
  }
}
