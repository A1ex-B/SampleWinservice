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
        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            //var currentError = errorArgs.ErrorContext.Error.Message;
            //errorArgs.ErrorContext.Handled = true;
        }
        public async Task<Receipt> Read(string filename)
        {
            Receipt receipt;

            try
            {
                using (var reader = new StreamReader(filename))
                {
                    var data = await reader.ReadToEndAsync();
                    receipt = JsonConvert.DeserializeObject<Receipt>(data);
                }
            }
            catch(Newtonsoft.Json.JsonSerializationException)
            {
                return null;
            }
            return receipt;
        }
    }
}
