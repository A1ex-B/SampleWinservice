using System.Threading.Tasks;
using Proxy;

namespace Service
{
    public interface IFileProcessor
    {
        Task<Receipt> Process(string filename);
    }
}