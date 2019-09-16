using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WCFService;

namespace Service
{
    public class ReceiptModelJson
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string Number { get; set; }
        [JsonProperty(Required = Required.Always)]
        public decimal Summ { get; set; }
        [JsonProperty(Required = Required.Always)]
        public decimal Discount { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string[] Articles { get; set; }
        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"Id: {Id}\n");
            s.Append($"Number: {Number}\n");
            s.Append($"Summ: {Summ}\n");
            s.Append($"Discount: {Discount}\n");
            s.Append("Articles: [");
            for (int i = 0; i < Articles.Length; i++)
            {
                s.Append($"\"{Articles[i]}\"");
                if (i < Articles.Length - 1)
                {
                    s.Append(", ");
                }
            }
            s.Append("]\n");
            return s.ToString();
        }
        public static implicit operator Receipt(ReceiptModelJson model)
        {
            return model != null ? new Receipt
            {
                Articles =  model.Articles,
                Discount = model.Discount,
                Id = model.Id,
                Number = model.Number,
                Summ = model.Summ
            }: null;
        }
    }
}
