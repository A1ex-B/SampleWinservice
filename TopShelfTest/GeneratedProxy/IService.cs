using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShelfTest
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IService")]
    public interface IService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/PutReceipt", ReplyAction = "http://tempuri.org/IService/PutReceiptResponse")]
        void PutReceipt(Receipt receipt);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/PutReceipt", ReplyAction = "http://tempuri.org/IService/PutReceiptResponse")]
        System.Threading.Tasks.Task PutReceiptAsync(Receipt receipt);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/GetReceipts", ReplyAction = "http://tempuri.org/IService/GetReceiptsResponse")]
        void GetReceipts(int number);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/GetReceipts", ReplyAction = "http://tempuri.org/IService/GetReceiptsResponse")]
        System.Threading.Tasks.Task GetReceiptsAsync(int number);
    }
}
