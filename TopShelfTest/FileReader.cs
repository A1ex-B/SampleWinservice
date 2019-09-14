using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Proxy;

namespace Service
{
    public class FileReader : IFileReader
    {
        public FileReader()
        {

        }
        public async Task<Receipt> ReadAsync(string filename, int attempts, int delay)
        {
            Receipt receipt = null;
            int i;
            for (i = 0; i < attempts; i++)
            {
                try
                {
                    using (var reader = new StreamReader(filename))
                    {
                        var data = await reader.ReadToEndAsync();
                        receipt = JsonConvert.DeserializeObject<Receipt>(data);
                        break;
                    }
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    return null;
                }
                catch(System.IO.IOException)
                {
                    continue;
                }
            }
            return receipt;
        }
    }
}
