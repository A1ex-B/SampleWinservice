using System.Threading.Tasks;
using WCFService;

namespace Service
{
    public interface IReceiptSender
    {
        Task SendAsync(Receipt receipt);
    }
}