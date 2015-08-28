using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using NHibernate.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContC.domain.entities.DTO;

namespace ContC.domain.services.Implementations
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository()
        {

        }


        public IList<Funcionario> GetByEmpresa(int empresaId)
        {
            return this.SessaoAtual.Query<FuncionarioEndereco>()
                .Where(p => p.Empresa.Id == empresaId)
                .Select(p => p.Funcionario).ToList();
        }

        public FuncionariosDTO GetByEmpresaTipoPagamentoLider(int empresaId)
        {
            FuncionariosDTO fdto = new FuncionariosDTO();
            fdto.EmpresaId = empresaId;

            fdto.TipoRegimeFuncionarios = this.SessaoAtual.Query<TipoRegimeFuncionario>()
                 .Select(p =>
                     new FuncionarioTipoRegimeFuncionarioDTO()
                     {
                         Id = p.Id,
                         Nome = p.Nome
                     }).ToList();

            fdto.TipoPagamentos = this.SessaoAtual.Query<TipoPagamento>()
                 .Select(p =>
                     new FuncionarioTipoPagamentoDTO()
                     {
                         Id = p.Id,
                         Nome = p.Nome
                     }).ToList();

            fdto.Lideres = (from a in this.SessaoAtual.Query<FuncionarioEndereco>()
                            where a.Funcionario.Lider == null && a.Empresa.Id == empresaId
                            select
                                new FuncionarioLiderDTO()
                                {
                                    Id = a.Funcionario.Id,
                                    Nome = a.Funcionario.Nome
                                }).ToList();

            return fdto;

        }


        public IList<Funcionario> GetByEmpresa(int empresaId, int tipoPagamento, string liderId)
        {
            switch (liderId)
            {
                case "0":
                    return
                        this.SessaoAtual.Query<FuncionarioEndereco>()
                    .Where(p => p.Empresa.Id == empresaId && p.Funcionario.TipoPagamento.Id == tipoPagamento)
                    .Select(p => p.Funcionario).ToList();
                case "":
                case null:
                    return
                        this.SessaoAtual.Query<FuncionarioEndereco>()
                    .Where(p => p.Empresa.Id == empresaId && p.Funcionario.TipoPagamento.Id == tipoPagamento && p.Funcionario.Lider == null)
                    .Select(p => p.Funcionario).ToList();
                default:
                    return
                        this.SessaoAtual.Query<FuncionarioEndereco>()
                     .Where(p => p.Empresa.Id == empresaId && p.Funcionario.TipoPagamento.Id == tipoPagamento && p.Funcionario.Lider != null && p.Funcionario.Lider.Id == Convert.ToInt32(liderId))
                     .Select(p => p.Funcionario).ToList();
            }

        }
    }
}
