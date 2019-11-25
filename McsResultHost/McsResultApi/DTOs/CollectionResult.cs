using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi.DTOs {
  public class CollectionResult {
    public string RequestReference { get; set; } = "";
    public string Mpan { get; set; } = "";
    public string MeterType { get; set; } = "";
    public string RemoteAddress { get; set; } = "";
    public string ComsSettings { get; set; } = "";
    public string OutstationAddress { get; set; } = "";
    public string Password { get; set; } = "";
    public bool AdjustTime { get; set; }
    public int SurveyDays { get; set; }
    public DateTime? SurveyDate { get; set; }
    public string Result { get; set; } = "";
    public DateTime? CollectionStartTime { get; set; }
    public DateTime? CollectionEndTime { get; set; }
    public DateTime? ConnectionStartTime { get; set; }
    public DateTime? ConnectionEndTime { get; set; }
    public string SerialNumber { get; set; } = "";
    public string MeterTime { get; set; } = "";
    public string TimeAdjustmentResult { get; set; } = "";
    public List<string> StatusEvents { get; set; }
    public List<RegisterValue> RegisterValues { get; set; } = new List<RegisterValue>();
    public List<RegisterSurveyData> SurveyData { get; set; } = new List<RegisterSurveyData>();
  }
}
