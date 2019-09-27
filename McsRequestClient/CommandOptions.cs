using CommandLine;

namespace Coherent.McsRequestClient {
  public class CommandOptions {
    [Option('i', "input-file", Required = true, HelpText = "Input file containing collection requests is CSV format [as used in MTS].")]
    public string InputFile { get; set; }
    [Option('p', "priority", Required = false, Default = 1, HelpText = "Priority that will be assigned to all requests. ")]
    public int Priority { get; set; }
  }
}
