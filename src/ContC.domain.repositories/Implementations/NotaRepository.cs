using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using ContC.domain.entities.DTO;

namespace ContC.domain.services.Implementations
{
    public class NotaRepository : Repository<Nota>, INotaRepository
    {
        public IList<Nota> GetNotasByEmpresaUsuario(int empresaId, string email)
        {
            email = email.ToUpper();


            // TODO: Testar
            List<Nota> notas = (from a in this.SessaoAtual.Query<Nota>()
                                where a.Empresa.Id == empresaId && a.Cadastrado.Funcionario.Email.ToUpper().Equals(email)
                                && a.Concluido == null
                                select a).ToList();

            notas.AddRange((from a in this.SessaoAtual.Query<NotaUsuario>()
                            where a.Usuario.Funcionario.Email.ToUpper().Equals(email) && a.Lista.Empresa.Id == empresaId
                            && a.Lista.Concluido == null
                            select a.Lista).ToList());

            return notas;
        }


        public IList<UsuarioDTO> GetUsuariosByNota(int notaSelecionada)
        {

            return (from a in this.SessaoAtual.Query<NotaUsuario>()
                            where a.Lista.Id == notaSelecionada
                            select new UsuarioDTO(){Usuario = a.Usuario, ECriador=false}).ToList();

            
        }
    }
}
