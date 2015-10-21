using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface INotaService : IService<Nota>
    {
        IList<Nota> GetNotasByEmpresaUsuario(int empresaId, string email);

        IList<NotaItem> GetItensByNotas(int notaId);

        void InsertNota(Nota nota, string email);

        void InsertItem(NotaItem ni, string email);

        NotaItem CheckItem(int itemNotaId, bool ischecked, string email);

        void RemoveItem(int itemNotaId, string p);

        void ConcluirNota(int notaId, string p);

        IList<UsuarioDTO> GetUsuariosByNota(int notaSelecionada);

        void RemoverNotaUsuarios(int notaId, int usuarioId);

        NotaUsuario AdicionarNotaUsuario(int notaId, string usuario);
    }
}
