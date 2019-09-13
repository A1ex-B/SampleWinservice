using System.Threading.Tasks;
using Proxy;

namespace Service
{
    public interface IFileReader
    {
        Task<Receipt> Read(string filename);
    }
}