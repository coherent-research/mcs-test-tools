using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi {
  public static class ApiHost {
    public static void Start(IResultProcessor resultProcessor, int listeningPort) {
      var host = new WebHostBuilder()
        .UseUrls($"http://*:{listeningPort}")
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureServices((c, s) => {
          s.AddSingleton<IResultProcessor>(resultProcessor);
        })
        .UseStartup<WebApiStartup>()
        .Build();
      host.Start();
    }
  }
}
