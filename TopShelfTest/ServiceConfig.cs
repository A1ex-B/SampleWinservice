using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceConfig
    {
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceDescription { get; set; }
        public string InputFolder { get; set; }
        public string CompleteFolder { get; set; }
        public string GarbageFolder { get; set; }
        public string FileExtension { get; set; }
    }
}
