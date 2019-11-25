using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost {
  public class CommandOptions {
    [Option('p', "port", Required = true, HelpText = "TCP listening port.")]
    public int Port { get; set; }
    [Option('d', "directory", Required = false, HelpText = "Directory to store result files. If omitted no files will be output. ")]
    public string Directory { get; set; }
  }
}
