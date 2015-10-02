using ContC.Communication.Model;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.Repositories.Mapping.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ContC.servicebus.application.Extension.restful
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Receitas : IReceitas
    {
        IReceitaService _ireceitaService;
        public Receitas(IReceitaService ireceitaService)
        {
            _ireceitaService = ireceitaService;

        }

        public Communication.Model.ReceitaComResponse Import(Communication.Model.ReceitaCom value)
        {
            try
            {
                Receita r = new Receita();
                r.DataCadastro = value.DataCadastro;
                r.DataRecebimento = value.DataCadastro;
                r.Descricao = value.Descricao;
                r.Endereco = new Empresa() { Id = Convert.ToInt32(value.EmpresaId) };
                r.TipoReceita = new TipoReceita() { Descricao = value.TipoReceita };
                r.Usuario = new Usuario() { Id = 2 };
                r.Valor = value.Valor;
                r.CommunicationId = value.CommunicationId;

                return InsertReceitas(r);
            }
            catch (Exception ex)
            {
                return new ReceitaComResponse()
                {
                    FoiProcessado = false,
                    Json = ex.Message
                };
                
            }
        }

        private ReceitaComResponse InsertReceitas(Receita r)
        {
            try
            {
                UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
                _ireceitaService.Insert(r);
                UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
                return new ReceitaComResponse()
                {
                    FoiProcessado = true
                };
            }
            catch (Exception ex)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                return new ReceitaComResponse()
                {
                    FoiProcessado = false,
                    Json = ex.Message
                };
            }
        }
    }
}
