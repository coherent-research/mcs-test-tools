using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsRequestClient {
  public class FileFormatException: Exception {
    public FileFormatException() {
      Details = "";
    }
    public FileFormatException(string message)
      : base(message) {
      Details = "";
    }
    public FileFormatException(string message, string details) : base(message) {
      Details = details;
    }
    public string Details { get; set; }
  }
}
