using ContC.CorssCutting.Exceptions;
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
        INotaUsuarioRepository _iUsuarioNotaRepository;

        public NotaService(INotaRepository repository, INotaItemRepository iNotaItemRepository, IUsuarioService usuarioService, INotaUsuarioRepository iUsuarioNotaRepository)
        {
            base._repository = repository;
            _iNotaItemRepository = iNotaItemRepository;
            _usuarioService = usuarioService;
            _iUsuarioNotaRepository = iUsuarioNotaRepository;
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


        public IList<entities.DTO.UsuarioDTO> GetUsuariosByNota(int notaSelecionada)
        {
            Nota ni = _repository.Find(notaSelecionada);
            List<entities.DTO.UsuarioDTO> users = new List<entities.DTO.UsuarioDTO>();
            users.Add(new entities.DTO.UsuarioDTO() { ECriador = true, Usuario = ni.Cadastrado });

            users.AddRange(((INotaRepository)_repository).GetUsuariosByNota(notaSelecionada));

            return users;
        }

        public void RemoverNotaUsuarios(int notaId, int usuarioId)
        {
            NotaUsuario nu = _iUsuarioNotaRepository.GetNotaUsuario(notaId, usuarioId);
            _iUsuarioNotaRepository.Delete(nu);
        }

        public NotaUsuario AdicionarNotaUsuario(int notaId, string usuario)
        {
            // TODO: Testar
            Usuario user = _usuarioService.GetUsuarioFetchFuncionario(usuario);
            if (user == null)
            {
                throw new ExceptionMessage("O Usuario não Existe");
            }
            NotaUsuario nu = _iUsuarioNotaRepository.GetNotaUsuario(notaId, user.Id);
            if (nu != null)
            {
                throw new ExceptionMessage("Esse Usuario já esta cadastrado nessa lista");
            }


            Nota nota = _repository.Find(notaId);
            nu = new NotaUsuario()
            {
                Usuario = user,
                Lista = nota
            };

            _iUsuarioNotaRepository.Insert(nu);

            return nu;
        }
    }
}
