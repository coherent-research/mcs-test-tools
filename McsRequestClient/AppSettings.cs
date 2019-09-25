using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsRequestClient {
  public class AppSettings {
    public string McsRequestUrl { get; set; } = "http://localhost:5001";
    public string McsResultUrl { get; set; } = "http://localhost:5000";
  }
}
