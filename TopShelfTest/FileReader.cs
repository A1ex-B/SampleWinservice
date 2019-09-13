using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proxy;

namespace Service
{
    public class FileReader : IFileReader
    {
        public FileReader()
        {
        }

        public async Task<Receipt> Read(string filename)
        {
            Receipt receipt;
            using (var reader = new StreamReader(filename))
            {
                var data = await reader.ReadToEndAsync();
                receipt = JsonConvert.DeserializeObject<Receipt>(data);
            }
            return receipt;
        }
    }
}
