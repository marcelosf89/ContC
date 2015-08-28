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
    public class EnderecoService : Service<Endereco>, IEnderecoService
    {
        public IEmpresaRepository _empresaRepository;
        public EnderecoService(IEnderecoRepository repository, IEmpresaRepository empresaRepository)
        {
            base._repository = repository;
            _empresaRepository = empresaRepository;
        }


        public override void Insert(ContC.domain.entities.Models.Endereco entity)
        {
            base.Insert(entity);
        }



        public IList<Empresa> GetAllEmpresaByGrupo(int grupoId)
        {
            return _empresaRepository.GetAllEmpresaByGrupo(grupoId);
        }


        public IList<Endereco> GetByEmpresa(int empresaId)
        {
            return ((IEnderecoRepository)_repository).GetByEmpresa(empresaId);
        }


        public IList<Empresa> GetAllEmpresaByUser(string email)
        {
            return ((IEnderecoRepository)_repository).GetAllEmpresaByUser(email);
        }
    }
}
