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
    public class NotaService : Service<Nota>, INotaService
    {
        INotaItemRepository _iNotaItemRepository;
        public NotaService(INotaRepository repository, INotaItemRepository iNotaItemRepository)
        {
            base._repository = repository;
            _iNotaItemRepository = iNotaItemRepository;
        }           
        

        public IList<Nota> GetNotasByEmpresaUsuario(int empresaId, string email)
        {
            return ((INotaRepository)_repository).GetNotasByEmpresaUsuario(empresaId, email);
        }


        public IList<NotaItem> GetItensByNotas(int notaId)
        {
            return _iNotaItemRepository.GetItensByNotas(notaId);
        }
    }
}
