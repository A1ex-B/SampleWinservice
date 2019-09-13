using System;
using System.Timers;

namespace TopShelfTest
{
    public class TownCrier : ITownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(10000) { AutoReset = true };
            _timer.Elapsed += async (sender, eventArgs) =>
            {
                Console.WriteLine("It is {0} and all is well", DateTime.Now);
                using (var service = new ServiceClient())
                {
                    await service.GetReceiptsAsync(11);
                }
            };
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
