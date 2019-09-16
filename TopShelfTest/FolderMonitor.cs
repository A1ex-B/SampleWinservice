using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WCFService;

namespace Service
{
    public class FolderMonitor : IService, IDisposable
    {
        private readonly ServiceConfig _config;
        private readonly IFileProcessor _fileProcessor;
        private readonly IReceiptSender _receiptSender;
        private readonly ILogger _logger;

        private readonly FileSystemWatcher _watcher;
        public FolderMonitor(IConfigLoader loader, IFileProcessor fileProcessor, IReceiptSender receiptSender, ILogger logger, IDirectoryChecker directoryChecker)
        {
            try
            {
                directoryChecker?.CheckDirectories();
            }
            catch (Exception ex)
            {
                _logger.Log($"{ex.GetType()}:\n{ex.Message}");
                Console.WriteLine($"{ex.GetType()}:\n{ex.Message}");
                throw ex;
            }
            _fileProcessor = fileProcessor ?? throw new ArgumentNullException(nameof(fileProcessor));
            _config = loader.Load();
            _watcher = new FileSystemWatcher(_config.InputFolder);
            _watcher.Created += OnCreatedFile;
            _receiptSender = receiptSender ?? throw new ArgumentNullException(nameof(receiptSender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            //System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        }
        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
        }
        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
        }

        private async void OnCreatedFile(object sender, FileSystemEventArgs e)
        {
            Receipt receipt = null;
            try
            {
                _logger.Log($"Fired OnCreatedFile...");
                receipt = await _fileProcessor.Process(e.FullPath);
                ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex}:{ex.Message}");
                _logger.Log($"Exception {ex}:{ex.Message}");
                //Logger.Log(...);
                //throw;
            }
            if (receipt == null)
            {
                Console.WriteLine("Bad file!");
                // Logget.Log(...);
            } else
            {
                Console.WriteLine("Receipt processed ok!");
                _logger.Log("Receipt processed ok!");
                try
                {
                    await _receiptSender.SendAsync(receipt);
                    _logger.Log($"Sent \n{receipt}");
                }
                catch (System.ServiceModel.CommunicationObjectFaultedException ex)
                {
                    Console.WriteLine($"Ошибка связи с WCF-сервисом! \n{ex.GetType()}:\n{ex.Message}");
                    _logger.Log($"Ошибка связи с WCF-сервисом! \n{ex.GetType()}:\n{ex.Message}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Неизвестное исключение в момент связи с WCF-сервисом:");
                    Console.WriteLine($"{ex.GetType()}:\n{ex.Message}");
                    _logger.Log("Неизвестное исключение в момент связи с WCF-сервисом:");
                    _logger.Log($"{ex.GetType()}:\n{ex.Message}");
                }
            }
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _watcher.Dispose();
                    //_cancellationTokenSource.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FolderMonitor()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
