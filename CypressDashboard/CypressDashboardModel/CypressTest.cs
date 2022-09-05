using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CypressDashboardModel
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Err
    {
        public string? message { get; set; }
        public string? estack { get; set; }
        public object? diff { get; set; }
    }

    public class Id
    {
        [JsonProperty("$oid")]
        public string? Oid { get; set; }
    }

    public class Marge
    {
        public Options? options { get; set; }
        public string? version { get; set; }
    }

    public class Meta
    {
        public Mocha? mocha { get; set; }
        public Mochawesome? mochawesome { get; set; }
        public Marge? marge { get; set; }
    }

    public class Mocha
    {
        public string? version { get; set; }
    }

    public class Mochawesome
    {
        public Options? options { get; set; }
        public string? version { get; set; }
    }

    public class Options
    {
        public bool? quiet { get; set; }
        public string? reportFilename { get; set; }
        public bool? saveHtml { get; set; }
        public bool? saveJson { get; set; }
        public string? consoleReporter { get; set; }
        public bool? useInlineDiffs { get; set; }
        public bool? code { get; set; }
        public bool? charts { get; set; }
        public bool? overwrite { get; set; }
        public bool? html { get; set; }
        public bool? json { get; set; }
        public string? reportDir { get; set; }
    }

    public class Result
    {
        public string? uuid { get; set; }
        public string? title { get; set; }
        public string? fullFile { get; set; }
        public string? file { get; set; }
        public List<object>? beforeHooks { get; set; }
        public List<object>? afterHooks { get; set; }
        public List<object>? tests { get; set; }
        public List<Suite>? suites { get; set; }
        public List<object>? passes { get; set; }
        public List<object>? failures { get; set; }
        public List<object>? pending { get; set; }
        public List<object>? skipped { get; set; }
        public int? duration { get; set; }
        public bool? root { get; set; }
        public bool? rootEmpty { get; set; }
        public int? _timeout { get; set; }
    }

    public class CypressTest
    {

        public MongoDB.Bson.ObjectId _id { get; set; }
        public Stats? stats { get; set; }
        public List<Result>? results { get; set; }
        public Meta? meta { get; set; }
    }

    public class Stats
    {
        public string? uid { get; set; }
        public int? suites { get; set; }
        public int? tests { get; set; }
        public int? passes { get; set; }
        public int? pending { get; set; }
        public int? failures { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int? duration { get; set; }
        public int? testsRegistered { get; set; }
        public double? passPercent { get; set; }
        public double? pendingPercent { get; set; }
        public int? other { get; set; }
        public bool? hasOther { get; set; }
        public int? skipped { get; set; }
        public bool? hasSkipped { get; set; }
    }

    public class Suite
    {
        public string? uuid { get; set; }
        public string? title { get; set; }
        public string? fullFile { get; set; }
        public string? file { get; set; }
        public List<object>? beforeHooks { get; set; }
        public List<object>? afterHooks { get; set; }
        public List<Test>? tests { get; set; }
        public List<object>? suites { get; set; }
        public List<string>? passes { get; set; }
        public List<object>? failures { get; set; }
        public List<object>? pending { get; set; }
        public List<object>? skipped { get; set; }
        public int? duration { get; set; }
        public bool? root { get; set; }
        public bool? rootEmpty { get; set; }
        public int? _timeout { get; set; }
    }

    public class Test
    {
        public string? title { get; set; }
        public string? fullTitle { get; set; }
        public object? timedOut { get; set; }
        public int? duration { get; set; }
        public string? state { get; set; }
        public string? speed { get; set; }
        public bool? pass { get; set; }
        public bool? fail { get; set; }
        public bool? pending { get; set; }
        public object? context { get; set; }
        public string? code { get; set; }
        public Err? err { get; set; }
        public string? uuid { get; set; }
        public string? parentUUID { get; set; }
        public bool? isHook { get; set; }
        public bool? skipped { get; set; }
    }


}

