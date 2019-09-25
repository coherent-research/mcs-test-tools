using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi.ConfigurationExtensions {
  public static class JsonExtensions {
    public static void ConfigureJson(this IMvcCoreBuilder mvc) {
      mvc.AddJsonFormatters();
      mvc.AddJsonOptions(options => {
        options.SerializerSettings.Converters.Add(new StringEnumConverter {
          CamelCaseText = true
        });
      });
    }
  }
}
