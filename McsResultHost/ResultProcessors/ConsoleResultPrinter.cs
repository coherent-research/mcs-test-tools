using Coherent.McsResultHost.McsResultApi;
using Coherent.McsResultHost.McsResultApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coherent.McsResultHost.ResultProcessors {
  public class ConsoleResultPrinter: IResultProcessor {
    public ConsoleResultPrinter(object consoleLock, IResultProcessor nextProcessor = null) {
      this.consoleLock = consoleLock;
      this.nextProcessor = nextProcessor;
    }
    public void Process(CollectionResult collectionResult) {
      lock (consoleLock) {
        var fg = Console.ForegroundColor;
        var timestamp = DateTime.Now.ToString(TimeFmt);
        SetConsoleColour(collectionResult.Result);
        Console.WriteLine(Line);
        Console.WriteLine($"Result received {timestamp}");
        Console.WriteLine(Line);
        Console.WriteLine($"Reference: {collectionResult.RequestReference}");
        Console.WriteLine($"Result: {collectionResult.Result}");
        var registerValues = FormatRegisterValues(collectionResult.RegisterValues);
        Console.WriteLine("Register values:");
        Console.WriteLine($"{registerValues}");
        var surveyData = FormatSurveyData(collectionResult.SurveyData);
        Console.WriteLine("Survey data:");
        Console.WriteLine($"{surveyData}");
        Console.WriteLine(Line);
        Console.WriteLine();
        Console.ForegroundColor = fg;
      }
      if(nextProcessor != null) {
        nextProcessor.Process(collectionResult);
      }    
    }


    static void SetConsoleColour(string result) {
      if (result.ToUpper() == "SUCCESS") {
        Console.ForegroundColor = ConsoleColor.Green;
      }
      else if (result.ToUpper() == "PARTIAL SUCCESS") {
        Console.ForegroundColor = ConsoleColor.Yellow;
      }
      else {
        Console.ForegroundColor = ConsoleColor.Red;
      }
    }
    static string FormatRegisterValues(IEnumerable<RegisterValue> registerValues) {
      if (registerValues.Any()) {
        return string.Join(Environment.NewLine, registerValues.Select(rv => $"{rv.Name}: {rv.Value} {rv.Units}"));
      }
      else {
        return "None";
      }
    }
    static string FormatSurveyData(IEnumerable<RegisterSurveyData> surveyData) {
      if (surveyData.Any()) {
        return string.Join(Environment.NewLine, surveyData.Select(rsd => $"{rsd.Name}: {FormatRegisterSurveyData(rsd.Readings)}"));
      }
      else {
        return "None";
      }
    }
    static string FormatRegisterSurveyData(IEnumerable<ReadingValue> registerSurveyData) {
      if (registerSurveyData.Any()) {
        var count = registerSurveyData.Count();
        var start = registerSurveyData.First().Timestamp.ToString(TimeFmt);
        var finish = registerSurveyData.Last().Timestamp.AddMinutes(30).ToString(TimeFmt);
        return $"{count} readings from {start} to {finish}";
      }
      else {
        return "None";
      }
    }

    const string Line = "====================================================================================================";
    const string TimeFmt = "yyyy-MM-dd HH:mm:ss";
    readonly object consoleLock;
    readonly IResultProcessor nextProcessor;
  }
}
