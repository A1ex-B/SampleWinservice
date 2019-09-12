using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShelfTest
{

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public class ServiceClient : System.ServiceModel.ClientBase<IService>, IService
    {

        public ServiceClient()
        {
        }

        public ServiceClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public ServiceClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
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
