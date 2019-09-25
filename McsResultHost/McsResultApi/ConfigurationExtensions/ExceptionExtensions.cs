using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Coherent.McsResultHost.McsResultApi.ConfigurationExtensions {
  public static class ExceptionExtensions {
    public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app) {
      app.UseGlobalExceptionHandler(x => {
        x.ContentType = "application/json; charset=utf-8";
        x.Map<ArgumentNullException>().ToStatusCode(StatusCodes.Status400BadRequest)
          .WithBody((ex, context) => {
            return JsonConvert.SerializeObject(new {
              details = ex.Message
            });
          });
        x.Map<Exception>().ToStatusCode(StatusCodes.Status500InternalServerError)
              .WithBody((ex, context) => {
                return JsonConvert.SerializeObject(new {
                  details = ex.Message
                });
              });
      });
    }
  }
}
