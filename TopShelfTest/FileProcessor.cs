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
        private readonly int _delay;
        private readonly int _attempts;
        private readonly ILogger _logger;

        public FileProcessor(IConfigLoader loader, IFileReader fileReader, ILogger logger)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }
            _config = loader.Load();
            _delay = _config.DelayForAnotherAttempt;
            _attempts = _config.AttempsToAccessFilesystem;
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        private async Task<int> TryMoveAsync(string sourceFullFileName, string destFullFileName)
        {
            int i = 0;
            string destPath = Path.GetDirectoryName(destFullFileName);
            string destFilename = Path.GetFileName(destFullFileName);

            for (; i < _attempts; i++)
            {
                try
                {
                    File.Move(sourceFullFileName, Path.Combine(destPath, destFilename));
                    return i;
                }
                catch (System.IO.IOException ex)
                {
                    if (ex.Message == "Cannot create a file when that file already exists.\r\n")
                    {
                        destFilename = Path.GetFileNameWithoutExtension(destFilename) + $"-{DateTime.Now.Ticks:X16}" + Path.GetExtension(destFilename);
                        continue;
                    }
                    await Task.Delay(_delay);
                }
            }
            return -i;
        }
        private async Task PutInGarbageAsync(string fullFileName)
        {
            var filename = Path.GetFileName(fullFileName);
            await TryMoveAsync(fullFileName, Path.Combine(_config.GarbageFolder, filename));
        }
        private async Task PutInCompleteAsync(string fullFileName)
        {
            var filename = Path.GetFileName(fullFileName);
            await TryMoveAsync(fullFileName, Path.Combine(_config.CompleteFolder, filename));
        }
        private bool ValidateName(string fullFileName)
        {
            return Path.GetExtension(fullFileName) == _config.FileExtension;
        }

        public async Task<Receipt> Process(string fullFileName)
        {
            Receipt result = null;
            _logger.Log($"Called 'Process(\"{fullFileName}\")'");
            if (!ValidateName(fullFileName))
            {
                await PutInGarbageAsync(fullFileName);
            }
            else
            {
                result = await _fileReader.ReadAsync(fullFileName, _attempts, _delay);
                if (result != null)
                {
                    await PutInCompleteAsync(fullFileName);
                }
                else
                {
                    await PutInGarbageAsync(fullFileName);
                }
            }
            return result;
        }
    }
}
