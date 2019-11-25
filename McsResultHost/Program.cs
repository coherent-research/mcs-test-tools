using Coherent.McsResultHost.McsResultApi;
using Coherent.McsResultHost.ResultProcessors;
using CommandLine;
using System;

namespace Coherent.McsResultHost {
  class Program {
    static void Main(string[] args) {
      Parser.Default.ParseArguments<CommandOptions>(args)
             .WithParsed<CommandOptions>(o => {
               Process(o);
             });

    }
    readonly static object consoleLock = new object();
    static void Process(CommandOptions options) {
      try {
        int listeningPort = options.Port;
        var fg = Console.ForegroundColor;
        ApiHost.Start(new ConsoleResultPrinter(consoleLock, new ResultFileWriter(options.Directory, consoleLock)), listeningPort);
        Console.WriteLine($"Awaiting collection results on port {listeningPort}");
        Console.WriteLine("Hit ENTER to exit");
        Console.ReadLine();
        Console.WriteLine();
        Console.ForegroundColor = fg;
      }
      catch (Exception ex) {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}
