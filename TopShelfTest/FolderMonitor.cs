using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Proxy;

namespace Service
{
    public class FolderMonitor : IService, IDisposable
    {
        private readonly ServiceConfig _config;
        private readonly IFileProcessor _fileProcessor;

        //private readonly CancellationTokenSource _cancellationTokenSource;
        //private Task _task;
        private readonly FileSystemWatcher _watcher;
        public FolderMonitor(IConfigLoader loader, IFileProcessor fileProcessor)
        {
            _fileProcessor = fileProcessor ?? throw new ArgumentNullException(nameof(fileProcessor));
            _config = loader.Load();
            _watcher = new FileSystemWatcher(_config.InputFolder);
            _watcher.Created += OnCreatedFile;

            //_cancellationTokenSource = new CancellationTokenSource();
            // Для теста как консольки
            //_task = new Task(async () =>
            //{
            //    using (var service = new WCFServiceClient())
            //    {
            //        while (!_cancellationTokenSource.IsCancellationRequested)
            //        {
            //            Console.WriteLine("It is {0} and all is well", DateTime.Now);
            //            //await service.GetReceiptsAsync(11); // Test!!! удалить.
            //            await Task.Delay(3000);
            //        }
            //    }
            //}, _cancellationTokenSource.Token);
        }
        public void Start()
        {
            //_task.Start();
            _watcher.EnableRaisingEvents = true;
        }
        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            //_cancellationTokenSource.Cancel();
        }

        private async void OnCreatedFile(object sender, FileSystemEventArgs e)
        {
            Receipt receipt = null;
            try
            {
                receipt = await _fileProcessor.Process(e.FullPath);
                ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex}:{ex.Message}");
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
