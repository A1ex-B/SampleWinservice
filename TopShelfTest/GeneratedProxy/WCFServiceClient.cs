using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public class WCFServiceClient : System.ServiceModel.ClientBase<IWCFService>, IWCFService
    {

        public WCFServiceClient()
        {
        }

        public WCFServiceClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public WCFServiceClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public WCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public WCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public void PutReceipt(Receipt receipt)
        {
            base.Channel.PutReceipt(receipt);
        }

        public System.Threading.Tasks.Task PutReceiptAsync(Receipt receipt)
        {
            return base.Channel.PutReceiptAsync(receipt);
        }

        public void GetReceipts(int number)
        {
            base.Channel.GetReceipts(number);
        }

        public System.Threading.Tasks.Task GetReceiptsAsync(int number)
        {
            return base.Channel.GetReceiptsAsync(number);
        }
    }
}
