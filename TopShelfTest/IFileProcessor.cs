using System.Threading.Tasks;
using WCFService;

namespace Service
{
    public interface IFileProcessor
    {
        Task<Receipt> Process(string filename);
    }
}