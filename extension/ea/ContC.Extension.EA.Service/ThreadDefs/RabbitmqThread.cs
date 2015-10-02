using ContC.Extension.EA.crosscutting.utilities.Config;
using ContC.Extension.EA.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Service.ThreadDefs
{
    public class RabbitmqThread : IDisposable
    {
        [ThreadStatic]
        private volatile RabbitmqService _rmq = new RabbitmqService();
        public RabbitmqService RabbitmqService
        {
            get
            {
                return _rmq;
            }
        }

        private RabbitmqConfig _rabbitmqConfig;

        public RabbitmqThread(RabbitmqConfig rabbitmqConfig)
        {
            this._rabbitmqConfig = rabbitmqConfig;
        }

        public void Start()
        {
            SetConfiguration();
        }

        private void SetConfiguration()
        {
            this.RabbitmqService.StartActivityMonitoring(this._rabbitmqConfig);
        }

        public void Dispose()
        {
            this.RabbitmqService.Stop();
        }
    }
}
