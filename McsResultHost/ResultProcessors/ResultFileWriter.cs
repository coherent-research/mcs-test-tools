using Coherent.McsResultHost.McsResultApi;
using Coherent.McsResultHost.McsResultApi.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Coherent.McsResultHost.ResultProcessors {
  public class ResultFileWriter: IResultProcessor {
    public ResultFileWriter(string directory, object consoleLock, IResultProcessor nextProcessor = null) {
      this.directory = directory;
      this.consoleLock = consoleLock;
      this.nextProcessor = nextProcessor;      
    }
    public void Process(CollectionResult collectionResult) {
      if(!string.IsNullOrEmpty(directory)) {
        lock(consoleLock) {
          var ts = DateTime.Now.ToString("yyyyMMddHHmmss");
          WriteResultFile(collectionResult, ts);
          if(collectionResult.SurveyData.Any()) {
            WriteDataFile(collectionResult, ts);
          }
        }
      }
      if(nextProcessor != null) {
        nextProcessor.Process(collectionResult);
      }
    }    

    private void WriteResultFile(CollectionResult collectionResult, string timestamp) {
      try {        
        var resultFilename = $"{directory}\\RESULT-{collectionResult.Mpan}-{timestamp}.txt";
        Console.WriteLine($"Writing results for test {collectionResult.RequestReference} to {resultFilename}");
        using(var sw = new StreamWriter(resultFilename)) {
          sw.WriteLine($"Request reference: {collectionResult.RequestReference}");
          sw.WriteLine($"MPAN: {collectionResult.Mpan}");
          sw.WriteLine($"Result: {collectionResult.Result}");
          sw.WriteLine($"Meter type: {collectionResult.MeterType}");
          sw.WriteLine($"Remote address: {collectionResult.RemoteAddress}");
          sw.WriteLine($"Coms settings: {collectionResult.ComsSettings}");
          sw.WriteLine($"Outstation address: {collectionResult.OutstationAddress}");
          sw.WriteLine($"Password: {collectionResult.Password}");
          sw.WriteLine($"Adjust time: {collectionResult.AdjustTime}");
          sw.WriteLine($"Survey days: {collectionResult.SurveyDays}");
          sw.WriteLine($"Survey date: {collectionResult.SurveyDate}");
          sw.WriteLine($"Collection start time: {FormatTimestamp(collectionResult.CollectionStartTime)}");
          sw.WriteLine($"Collection end time: {FormatTimestamp(collectionResult.CollectionEndTime)}");
          sw.WriteLine($"Connection start time: {FormatTimestamp(collectionResult.ConnectionStartTime)}");
          sw.WriteLine($"Connection end time: {FormatTimestamp(collectionResult.ConnectionEndTime)}");
          sw.WriteLine($"Serial number: {collectionResult.SerialNumber}");
          sw.WriteLine($"Meter time: {collectionResult.MeterTime}");
          sw.WriteLine($"Time adjustment result: {collectionResult.TimeAdjustmentResult}");
          sw.WriteLine($"Status flags: {string.Join(", ", collectionResult.StatusEvents)}");
          foreach(var rv in collectionResult.RegisterValues) {
            sw.WriteLine($"{rv.Name}: {rv.Value} {rv.Units}");
          }
        }
      }
      catch(Exception ex) {
        WriteError($"Error writing result file: {ex.Message}");
      }
    }
    private void WriteDataFile(CollectionResult collectionResult, string timestamp) {
      var dataFilename = $"{directory}\\DATA-{collectionResult.Mpan}-{timestamp}.csv";
      Console.WriteLine($"Writing survey data for test {collectionResult.RequestReference} to {dataFilename}");
      using(var sw = new StreamWriter(dataFilename)) {
        sw.WriteLine(DataHeader);
        foreach(var sd in collectionResult.SurveyData) {
          foreach(var r in sd.Readings) {
            sw.WriteLine($"{collectionResult.Mpan}, {sd.Name}, {FormatTimestamp(r.Timestamp)}, {r.Value}, {sd.Units}, {r.StatusFlags}");
          }
        }
      }
    }
    private void WriteError(string message) {
      var fg = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(message);
      Console.ForegroundColor = fg;
    }
    private string FormatTimestamp(DateTime timestamp) {
      return timestamp.ToString("yyyy-MM-dd HH:mm:ss");
    }
    private string FormatTimestamp(DateTime? timestamp) {
      if(timestamp.HasValue) {
        return FormatTimestamp(timestamp.Value);
      }
      else {
        return "";
      }
    }
    const string DataHeader = "MPAN, Register, Timestamp, Value, Unit, StatusFlags";
    readonly string directory;
    readonly IResultProcessor nextProcessor;
    readonly object consoleLock;
  }
}
