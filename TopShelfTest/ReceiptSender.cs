using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proxy;

namespace Service
{
    public class ReceiptSender : IReceiptSender
    {
        public ReceiptSender()
        {
        }

        public async Task SendAsync(Receipt receipt)
        {
            using (var WCFService = new WCFServiceClient())
            {
                await WCFService.PutReceiptAsync(receipt);
            }
        }
    }
}
