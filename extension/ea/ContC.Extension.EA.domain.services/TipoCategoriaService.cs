using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContC.Extension.EA.domain.entities.Models;
using ContC.Extension.EA.domain.services.ContraContC.Extension.EA;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ContC.Extension.EA.domain.services
{
    public class TipoCategoriaService : Service<TipoCategoria>, ITipoCategoriaService
    {

        private readonly IRepositoryAsync<TipoCategoria> _repository;

        public TipoCategoriaService(IRepositoryAsync<TipoCategoria> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public override void Insert(TipoCategoria entity)
        {
            Validar(entity);
            base.Insert(entity);
        }

        public override void Update(TipoCategoria entity)
        {
            Validar(entity);
            base.Update(entity);
        }

        private void Validar(TipoCategoria entity)
        {
            if (_repository.Query(q => q.Sigla.ToUpper() == entity.Sigla.ToUpper() && q.Id != entity.Id).Select().Any())
                throw new Exception("Já existe um tipo de categoria com esta sigla cadastrada no sistema");
            if (_repository.Query(q => q.Descricao.ToUpper() == entity.Descricao.ToUpper() && q.Id != entity.Id).Select().Any())
                throw new Exception("Já existe um tipo de categoria com esta descrição cadastrada no sistema");
        }


    }
}
