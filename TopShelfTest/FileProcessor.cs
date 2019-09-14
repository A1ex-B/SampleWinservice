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
        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
        private async Task<bool> WaitForFileAsync(FileInfo file, int attempts, int delay)
        {
            Console.WriteLine("Ждём, пока файл освободится...");
            for (int i = 0; i < attempts; i++)
            {
                if (!IsFileLocked(file))
                {
                    Console.WriteLine($"Успешно, ждали {i} попыток");
                    return true;
                } else
                {
                    await Task.Delay(delay);
                    Console.WriteLine("Ждём...");
                }
            }
            Console.WriteLine($"Неуспешно, ждали {attempts} попыток");
            return false;
        }
        private void PutInGarbage(string fullFileName)
        {
            File.Move(fullFileName, Path.Combine(_config.GarbageFolder, Path.GetFileName(fullFileName)));
        }
        private async Task PutInCompleteAsync(string fullFileName)
        {
            var filename = Path.GetFileName(fullFileName);
            var fileInfo = new FileInfo(fullFileName);
            var fileIsReady = await WaitForFileAsync(fileInfo, 5, 50);
            if (!fileIsReady)
            {
                throw new IOException($"Cant't acces the file {fullFileName}!");
            }
            try
            {
                File.Move(fullFileName, Path.Combine(_config.CompleteFolder, filename));
            }
            catch (System.IO.IOException ex)
            {
                if (ex.Message == "Cannot create a file when that file already exists.\r\n")
                {
                    filename = Path.GetFileNameWithoutExtension(filename) + $"-{DateTime.Now.Ticks:X16}" + Path.GetExtension(filename);
                    File.Move(fullFileName, Path.Combine(_config.CompleteFolder, filename));
                }
                else
                {
                    throw ex;
                }
            }
        }
        private bool ValidateName(string fullFileName)
        {
            return Path.GetExtension(fullFileName) == _config.FileExtension;
        }

        public async Task<Receipt> Process(string fullFileName)
        {
            Receipt result = null;
            if (!ValidateName(fullFileName))
            {
                PutInGarbage(fullFileName);
            }
            else
            {
                result = await _fileReader.Read(fullFileName);
                if (result != null)
                {
                    await PutInCompleteAsync(fullFileName);
                }
                else
                {
                    PutInGarbage(fullFileName);
                }
            }
            return result;
        }
    }
}
