using CommandLine;

namespace Coherent.McsRequestClient {
  public class CommandOptions {
    [Option('i', "input-file", Required = true, HelpText = "Input file containing collection requests is CSV format [as used in MTS].")]
    public string InputFile { get; set; }
  }
}
