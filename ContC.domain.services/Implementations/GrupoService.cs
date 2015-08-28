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
    public class GrupoService : Service<Grupo>, IGrupoService
    {
        private IUsuarioService _usuarioService;

        public GrupoService(IGrupoRepository repository, IUsuarioService usuarioService)
        {
            base._repository = repository;
            _usuarioService = usuarioService;
        }


        public override void Insert(ContC.domain.entities.Models.Grupo entity)
        {


            base.Insert(entity);
        }



        public Grupo GetByCode(string code)
        {
            throw new NotImplementedException();
        }


        public IList<Grupo> GetAllGrupo(Funcionario usuario)
        {
            return ((IGrupoRepository)_repository).GetAllGrupo(usuario.Email);
        }

        public IList<Grupo> GetAllGrupo(string nomeResponsavel)
        {
            return ((IGrupoRepository)_repository).GetAllGrupo(nomeResponsavel);
        }
    }
}
