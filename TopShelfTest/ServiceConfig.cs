using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service
{
    public class ServiceConfig
    {
        [JsonProperty(Required = Required.Always)]
        public string ServiceName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string ServiceDisplayName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string ServiceDescription { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string InputFolder { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string CompleteFolder { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string GarbageFolder { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string FileExtension { get; set; }
        [JsonProperty(Required = Required.Always)]
        public int AttempsToAccessFilesystem { get; set; }
        [JsonProperty(Required = Required.Always)]
        public int DelayForAnotherAttempt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string LogFileFullName{ get; set; }
    }
}
