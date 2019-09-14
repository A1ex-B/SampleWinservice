using System.Threading.Tasks;
using Proxy;

namespace Service
{
    public interface IReceiptSender
    {
        Task SendAsync(Receipt receipt);
    }
}