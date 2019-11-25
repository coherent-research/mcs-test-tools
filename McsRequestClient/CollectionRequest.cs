using System;

namespace Coherent.McsRequestClient {
  public class CollectionRequest {
    public string RequestReference { get; set; } = "";
    public string Mpan { get; set; } = "";
    public string ResponseUrl { get; set; } = "";
    public DateTime? DelayUntil { get; set; }
    public int Priority { get; set; } = 1;
    public string MeterType { get; set; } = "";
    public string RemoteAddress { get; set; } = "";
    public string ComsSettings { get; set; } = "";
    public string OutstationAddress { get; set; } = "";
    public string SerialNumber { get; set; } = "";
    public string Password { get; set; } = "";
    public int SurveyDays { get; set; }
    public DateTime? SurveyDate { get; set; }
    public bool AdjustTime { get; set; }
  }
}
