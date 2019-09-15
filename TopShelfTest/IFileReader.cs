using System.Threading.Tasks;
using WCFService;

namespace Service
{
    public interface IFileReader
    {
        Task<Receipt> ReadAsync(string filename, int attempts, int delay);
    }
}