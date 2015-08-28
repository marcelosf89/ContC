using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class BoletoService : Service<Boleto>, IBoletoService
    {
        public BoletoService(IBoletoRepository repository)
        {
            base._repository = repository;
        }


        public IList<ContasDTO> GetContasByDashboard(int empresaId, int quantidadesDia)
        {
            return ((IBoletoRepository)_repository).GetContasByDashboard(empresaId, quantidadesDia);
        }

        public void Insert(Boleto boleto, FileInfo fi)
        {
            if (!fi.Exists)
            {
                throw new Exception("O Arquivo não foi copiado para o servidor. Favor carrege o arquivo corretamente e tente de novo");
            }
            if (boleto.Fornecedor.Id <= 0)
            {
                throw new Exception("O Fornecedor é obrigatório");
            }

            base.Insert(boleto);
        }



        public IList<ContasDTO> GetContasByEmpresaPeriodo(int empresaId, DateTime inicio, DateTime termino)
        {
            return ((IBoletoRepository)_repository).GetContasByDashboard(empresaId, inicio, termino);
        }


        public void Remove(Boleto boleto)
        {
            if (String.IsNullOrEmpty(boleto.MotivoCancelamento))
            {
                throw new Exception("O Motivo tem que ser preenchido");
            }
            else if (boleto.UsuarioCancelamento == null)
            {
                throw new Exception("O Usuario de Cancelamento é obrigatorio... favor refaça o login e tente novamente");
            }

            boleto.DataCancelamento = DateTime.Now;
            base.Update(boleto);
        }
    }
}
