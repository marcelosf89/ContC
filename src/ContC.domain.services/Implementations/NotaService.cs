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
        IUsuarioService _usuarioService;
        public NotaService(INotaRepository repository, INotaItemRepository iNotaItemRepository, IUsuarioService usuarioService)
        {
            base._repository = repository;
            _iNotaItemRepository = iNotaItemRepository;
            _usuarioService = usuarioService;
        }


        public IList<Nota> GetNotasByEmpresaUsuario(int empresaId, string email)
        {
            return ((INotaRepository)_repository).GetNotasByEmpresaUsuario(empresaId, email);
        }


        public IList<NotaItem> GetItensByNotas(int notaId)
        {
            return _iNotaItemRepository.GetItensByNotas(notaId);
        }


        public void InsertNota(Nota nota, string email)
        {
            Usuario user = _usuarioService.GetUsuario(email);
            nota.Cadastrado = user;
            nota.Cadastro = DateTime.Now;

            this.Insert(nota);
        }


        public void InsertItem(NotaItem ni, string email)
        {
            Usuario user = _usuarioService.GetUsuario(email);
            ni.CadastradoPor = user;
            ni.Cadastro = DateTime.Now;

            _iNotaItemRepository.Update(ni);
        }


        public NotaItem CheckItem(int itemNotaId, bool ischecked, string email)
        {

            NotaItem ni = _iNotaItemRepository.Find(itemNotaId);
            if (ischecked)
            {
                Usuario user = _usuarioService.GetUsuario(email);
                ni.Concluido = DateTime.Now;
                ni.ConcluidoPor = user;
            }
            else
            {
                ni.Concluido = null;
                ni.ConcluidoPor = null;
            }
            _iNotaItemRepository.Update(ni);
            return ni;
        }


        public void RemoveItem(int itemNotaId, string email)
        {
            NotaItem ni = _iNotaItemRepository.Find(itemNotaId);
            Usuario user = _usuarioService.GetUsuario(email);
            ni.Cancelado = DateTime.Now;
            ni.CanceladoPor = user;
            _iNotaItemRepository.Update(ni);
        }


        public void ConcluirNota(int notaId, string email)
        {
            Nota ni = _repository.Find(notaId);
            Usuario user = _usuarioService.GetUsuario(email);
            ni.CanceladoPor = user;
            ni.Concluido = DateTime.Now;

            _repository.Update(ni);
        }
    }
}
