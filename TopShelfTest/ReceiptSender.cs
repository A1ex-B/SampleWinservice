using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService;

namespace Service
{
    public class ReceiptSender : IReceiptSender
    {
        public ReceiptSender()
        {
        }

        public async Task SendAsync(Receipt receipt)
        {
            using (var service = new ServiceClient())
            {
                await service.PutReceiptAsync(receipt);
            }
        }
    }
}
