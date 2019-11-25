using CommandLine;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Coherent.McsRequestClient {
  class Program {
    static void Main(string[] args) {
      IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json", true, false)
        .Build();
      var appSettings = new AppSettings();
      config.Bind(appSettings);

      Parser.Default.ParseArguments<CommandOptions>(args)
             .WithParsed<CommandOptions>(o => {
               Process(o, appSettings);
             });

    }
    static void Process(CommandOptions options, AppSettings settings) {
      try {
        Console.WriteLine($"Uploading file {options.InputFile} to {settings.McsRequestUrl}");
        if (!File.Exists(options.InputFile)) {
          throw new Exception($"Cannot find input file '{options.InputFile}'");
        }
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        var url = settings.McsRequestUrl + "/collection-request";

        var fileContent = File.ReadAllText(options.InputFile);
        var requests = RequestFileMapper.GetRequestsFromFile(options.InputFile);
        var acceptedCount = 0;
        foreach (var r in requests) {
          r.RequestReference = Guid.NewGuid().ToString();
          r.ResponseUrl = settings.McsResultUrl;
          r.Priority = options.Priority;
          OutputInfo($"Sending request {r.RequestReference} for meter: {r.Mpan} [{r.MeterType} - {r.RemoteAddress}]");
          var postResult = client.PostAsJsonAsync(url, r).Result;
          if (postResult.IsSuccessStatusCode) {
            OutputInfo("Request accepted");
            ++acceptedCount;
          }
          else {
            OutputInfo($"Request rejected: {postResult.Content.ReadAsStringAsync().Result}");
          }
        }
        OutputInfo($"{acceptedCount} requests have been accepted");
      }
      catch (Exception ex) {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    static void OutputInfo(string message) {
      Console.ForegroundColor = ConsoleColor.White;
      OutputText(message);
    }
    static void OutputError(string message) {
      Console.ForegroundColor = ConsoleColor.Red;
      OutputText(message);
    }
    static void OutputText(string message) {
      Console.WriteLine(message);
    }

    static HttpClient client = new HttpClient();
  }
}
