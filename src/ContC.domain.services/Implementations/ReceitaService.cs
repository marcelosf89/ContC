using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class ReceitaService : Service<Receita>, IReceitaService
    {
        ITipoReceitaRepository _tipoReceitaRepository;
        public ReceitaService(IReceitaRepository repository, ITipoReceitaRepository tipoReceitaRepository)
        {
            base._repository = repository;
            _tipoReceitaRepository = tipoReceitaRepository;
        }

        public override void Insert(ContC.domain.entities.Models.Receita entity)
        {
            if (((IReceitaRepository)_repository).HasByCommunicationId(entity.CommunicationId))
            {
                return;
            }
            
            TipoReceita tr = _tipoReceitaRepository.GetByDescricao(entity.TipoReceita.Descricao);
            if (tr == null)
            {
                _tipoReceitaRepository.Insert(entity.TipoReceita);
            }
            entity.TipoReceita = tr;

            base.Insert(entity);
        }


        public IList<entities.DTO.ReceitasDTO> GetReceitasByEmpresaPeriodo(int empresaId, DateTime inicio, DateTime final)
        {
            return ((IReceitaRepository)_repository).GetReceitasByEmpresaPeriodo(empresaId, inicio, final);
        }
    }
}
