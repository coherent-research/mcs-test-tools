using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi.DTOs {
  public class RegisterSurveyData {
    public string Name { get; set; }
    public string Units { get; set; }
    public ReadingValue[] Readings { get; set; }
  }
}
