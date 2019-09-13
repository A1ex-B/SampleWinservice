using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proxy;

namespace Service
{
    public class FileProcessor : IFileProcessor
    {
        private readonly ServiceConfig _config;
        private readonly IFileReader _fileReader;

        public FileProcessor(IConfigLoader loader, IFileReader fileReader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }
            _config = loader.Load();
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }
        private void PutInGarbage(string fullFileName)
        {
            File.Move(fullFileName, Path.Combine(_config.GarbageFolder, "\\" + Path.GetFileName(fullFileName)));
        }
        private void PutInComplete(string fullFileName)
        {
            File.Move(fullFileName, Path.Combine(_config.CompleteFolder, "\\" + Path.GetFileName(fullFileName)));
        }
        private bool ValidateName(string fullFileName)
        {
            return Path.GetExtension(fullFileName) == _config.FileExtension;
        }

        public async Task<Receipt> Process(string fullFileName)
        {
            if (ValidateName(fullFileName))
            {
                PutInGarbage(fullFileName);
                return null;
            }
            else
            {
                var result = await _fileReader.Read(fullFileName);
                PutInComplete(fullFileName);
                return result;
            }
        }
    }
}
