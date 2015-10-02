using ContC.Communication.Model.ServerToClient;
using ContC.crosscutting.utilities.RabbitMq;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class EmpresaService : Service<Empresa>, IEmpresaService
    {
        public EmpresaService(IEmpresaRepository repository)
        {
            base._repository = repository;
        }



        public IList<Empresa> GetAllEmpresaByGrupo(int grupoId)
        {
            return ((IEmpresaRepository)_repository).GetAllEmpresaByGrupo(grupoId);
        }


        public IList<Empresa> GetByEmpresa(int empresaId)
        {
            return ((IEmpresaRepository)_repository).GetByEmpresa(empresaId);
        }


        public IList<Empresa> GetAllEmpresaByUser(string email)
        {
            return ((IEmpresaRepository)_repository).GetAllEmpresaByUser(email);
        }



        public void SendCommunicationReceita(int empresaId)
        {
            Empresa emp = this.Find(empresaId);

            String host = ConfigurationManager.AppSettings["RabbitMQHost"];
            String user = ConfigurationManager.AppSettings["RabbitMQUser"];
            String pass = ConfigurationManager.AppSettings["RabbitMQPass"];
            String exchange = ConfigurationManager.AppSettings["RabbitMQExchange"];


            try
            {
                using (RabbitMQProducer rabbit = new RabbitMQProducer(host, user, pass, exchange, emp.RabbitmqQueue))
                {
                    GetValuesModel gvm = new GetValuesModel();
                    gvm.GetValuesEnum = GetValuesEnum.GetReceitas;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(GetValuesModel));
                        deserializer.WriteObject(ms, gvm);
                        rabbit.Produce(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("A Mensagem não foi enviado para o Rabbit.");
            }

        }
    }
}
