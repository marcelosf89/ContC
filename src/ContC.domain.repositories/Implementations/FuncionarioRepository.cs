using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using NHibernate.Linq;
using System.Linq;
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
            return (from f in this.SessaoAtual.Query<Funcionario>()
                    from e in f.Empresas
                    where e.Id == empresaId
                    select f).ToList();
        }

        public FuncionariosDTO GetByEmpresaTipoPagamentoLider(int empresaId)
        {
            FuncionariosDTO retorno = new FuncionariosDTO();
            retorno.EmpresaId = empresaId;

            retorno.TipoRegimeFuncionarios = this.SessaoAtual.Query<TipoRegimeFuncionario>()
                 .Select(p =>
                     new FuncionarioTipoRegimeFuncionarioDTO()
                     {
                         Id = p.Id,
                         Nome = p.Nome
                     }).ToList();

            retorno.TipoPagamentos = this.SessaoAtual.Query<TipoPagamento>()
                 .Select(p =>
                     new FuncionarioTipoPagamentoDTO()
                     {
                         Id = p.Id,
                         Nome = p.Nome
                     }).ToList();

            retorno.Lideres = (from f in this.SessaoAtual.Query<Funcionario>()
                            from e in f.Empresas
                            where f.Lider == null && e.Id == empresaId
                            select
                                new FuncionarioLiderDTO()
                                {
                                    Id = f.Id,
                                    Nome = f.Nome
                                }).ToList();

            return retorno;

        }


        public IList<Funcionario> GetByEmpresa(int empresaId, int tipoPagamento, string liderId)
        {
            switch (liderId)
            {
                case "0":
                    return
                        (from f in SessaoAtual.Query<Funcionario>()
                         from e in f.Empresas
                        where e.Id == empresaId && f.TipoPagamento.Id == tipoPagamento
                        select f).ToList();
                           
                case "":
                case null:
                    return 
                        (from f in SessaoAtual.Query<Funcionario>()
                         from e in f.Empresas
                        where e.Id == empresaId && f.TipoPagamento.Id == tipoPagamento && f.Lider == null
                       select f).ToList();
                default:
                    return
                        (from f in SessaoAtual.Query<Funcionario>()
                         from e in f.Empresas
                         where e.Id == empresaId && f.TipoPagamento.Id == tipoPagamento && f.Lider != null && f.Lider.Id == Convert.ToInt32(liderId)
                         select f).ToList();
            }

        }

        public TipoRegimeFuncionario GetRegime(int id)
        {
            return SessaoAtual.Get<TipoRegimeFuncionario>(id);
        }

        public TipoPagamento GetTipoPagamento(int id)
        {
            return SessaoAtual.Get<TipoPagamento>(id);
        }
    }
}
