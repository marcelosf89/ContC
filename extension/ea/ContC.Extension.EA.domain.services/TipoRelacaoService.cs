using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContC.Extension.EA.domain.entities.Models;
using ContC.Extension.EA.domain.repositories;
using ContC.Extension.EA.domain.services.ContraContC.Extension.EA;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ContC.Extension.EA.domain.services
{
    public class TipoRelacaoService : Service<TipoReceita>, ITipoRelacaoService
    {

        private readonly IRepositoryAsync<TipoReceita> _repository;

        public TipoRelacaoService(IRepositoryAsync<TipoReceita> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public TipoReceita ObterTipoDeRelacaoUsuarioEmpresa(Usuario usuario, Empresa empresa)
        {
            return _repository.ObterTipoDeRelacaoUsuarioEmpresa(usuario, empresa);
        }

        public override void Insert(TipoReceita entity)
        {
            Validar(entity);
            base.Insert(entity);
        }

        public override void Update(TipoReceita entity)
        {
            Validar(entity);
            base.Update(entity);
        }

        private void Validar(TipoReceita entity)
        {
            if (_repository.Query(q => q.Descricao.ToUpper() == entity.Descricao.ToUpper() && q.Id != entity.Id).Select().Any())
                throw new Exception("Já existe um tipo de relação com esta descrição cadastrada no sistema");
        }

    }
}
