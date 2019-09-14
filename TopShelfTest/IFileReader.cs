using System.Threading.Tasks;
using Proxy;

namespace Service
{
    public interface IFileReader
    {
        Task<Receipt> ReadAsync(string filename, int attempts, int delay);
    }
}