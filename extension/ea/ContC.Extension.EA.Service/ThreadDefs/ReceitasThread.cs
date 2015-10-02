using ContC.Extension.EA.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Service.ThreadDefs
{
    public class ReceitasThread : IDisposable
    {
        [ThreadStatic]
        private volatile ReceitasService _rmq = new ReceitasService();

        private domain.services.Contracts.IReceitaService _receitaServices;
        public ReceitasService ReceitasService
        {
            get
            {
                return _rmq;
            }
        }


        public ReceitasThread(domain.services.Contracts.IReceitaService _receitaServices)
        {
            this._receitaServices = _receitaServices;
        }

        public void Start()
        {
            SetConfiguration();
        }


        private void SetConfiguration()
        {
            this.ReceitasService.StartActivityMonitoring(this._receitaServices);
        }

        public void Dispose()
        {
            this.ReceitasService.Stop();
        }
    }
}
