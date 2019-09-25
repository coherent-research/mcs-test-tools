using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost {
  public class CommandOptions {
    [Option('p', "port", Required = true, HelpText = "TCP listening port.")]
    public int Port { get; set; }
  }
}
