using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Coherent.McsRequestClient {
  public class RequestFileMapper {
    public static List<CollectionRequest> GetRequestsFromFile(string filename) {
      try {
        using (var reader = new StreamReader(filename))
        using (var csv = new CsvReader(reader)) {
          var errorLines = new List<string>();
          csv.Configuration.RegisterClassMap<ColletionRequestClassMap>();
          csv.Configuration.HasHeaderRecord = true;

          var requests = csv.GetRecords<CollectionRequest>().ToList();
          return requests;
        }
      }
      catch (CsvHelperException ex) {
        throw new Exception($"Could not parse line {ex.ReadingContext.Row}");
      }
      catch (Exception) {
        throw;
      }
    }
  }
  public class ColletionRequestClassMap : ClassMap<CollectionRequest> {
    public ColletionRequestClassMap() {
      Map(m => m.Mpan).Name("MPAN");
      Map(m => m.MeterType).Name("METER_TYPE").Default("");
      Map(m => m.RemoteAddress).Name("REMOTE_ADDRESS");
      Map(m => m.ComsSettings).Name("COM_SETTINGS").Default("");
      Map(m => m.OutstationAddress).Name("OUTSTATION").Default("");
      Map(m => m.Password).Name("PASSWORD").Default("");
      Map(m => m.SurveyDays).Name("SURVEY_DAYS").Default(0);
      Map(m => m.SurveyDate).Name("SURVEY_DATE");
      Map(m => m.SerialNumber).Name("SERIAL_NUMBER");
    }
  }
}
