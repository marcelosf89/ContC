using ContC.Extension.EA.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Service.ThreadDefs
{
    public class ReceitasAutoThread : IDisposable
    {
        [ThreadStatic]
        private volatile ReceitasAutoCommunicationService _rmq = new ReceitasAutoCommunicationService();

        private domain.services.Contracts.IReceitaService _receitaServices;
        public ReceitasAutoCommunicationService ReceitasService
        {
            get
            {
                return _rmq;
            }
        }


        public ReceitasAutoThread(domain.services.Contracts.IReceitaService _receitaServices)
        {
            this._receitaServices = _receitaServices;
        }

        public void Start()
        {
            SetConfiguration();
        }


        private void SetConfiguration()
        {
            this.ReceitasService.StartActivityMonitoring();
        }

        public void Dispose()
        {
            this.ReceitasService.Stop();
        }
    }
}
