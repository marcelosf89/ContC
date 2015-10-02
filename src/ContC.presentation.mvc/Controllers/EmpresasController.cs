using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Models;
using ContC.presentation.mvc.Models.EmpresaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class EmpresasController : Controller
    {
        private IEmpresaService _empresaService;
        private IUsuarioService _usuarioService;
        private IGrupoService _grupoService;

        public EmpresasController(IEmpresaService empresaService, IUsuarioService usuarioService, IGrupoService grupoService)
        {
            _empresaService = empresaService;
            _usuarioService = usuarioService;
            _grupoService = grupoService;
        }
        // GET: Empresas
        public ActionResult Index()
        {
            IList<Grupo> grupos = _grupoService.GetAllGrupo(User.Identity.Name);

            IndexModel im = new IndexModel();
            im.SelectedGroupId = grupos.FirstOrDefault().Id;

            im.Grupos = grupos;

            return View(im);
        }

        public ActionResult Novo(int grupoId)
        {
            EmpresaNovoModel enm = new EmpresaNovoModel();
            enm.GroupId = grupoId;
            return View(enm);
        }

        public ActionResult EmpresasPartial(int grupoId)
        {
            IList<Empresa> le = _empresaService.GetAllEmpresaByGrupo(grupoId);
            EmpresaPartialModel epm = new EmpresaPartialModel();
            epm.GrupoId = grupoId;
            epm.Empresas = le;
            return View(epm);
        }

        public ActionResult Editar(int empresaId)
        {
            Empresa emp = _empresaService.GetByEmpresa(empresaId).FirstOrDefault();
            EmpresaNovoModel enm = new EmpresaNovoModel();
            enm.GroupId = emp.Grupo.Id;
            enm.EmpresaId = emp.Id;
            enm.EnderecoId = emp.Id;
            enm.NomeFantasia = emp.NomeFantasia;
            enm.RazaoSocial = emp.RazaoSocial;
            enm.Bairro = emp.Bairro;
            enm.Cidade = emp.Cidade;
            enm.CNPJ = emp.CNPJ;
            //enm.Codigo = emp.Empresa.c
            enm.CodigoPostal = emp.CodigoPostal;
            enm.Complemento = emp.Complemento;
            enm.Estado = emp.Estado;
            enm.Numero = emp.Numero;
            enm.Pais = emp.Pais;
            enm.Rua = emp.Rua;

            return View("Novo", enm);
        }

        public ActionResult Salvar(EmpresaNovoModel enm)
        {
            Grupo g = _grupoService.Find(enm.GroupId);

            Empresa en = new Empresa();

            if (enm.EnderecoId != default(int))
            {
                en = _empresaService.Find(enm.EnderecoId);
            }

            en.Id = enm.EmpresaId;
            en.RazaoSocial = enm.RazaoSocial;
            en.NomeFantasia = enm.NomeFantasia;
            en.Grupo = g;
            
            en.Rua = enm.Rua;
            en.CNPJ = enm.CNPJ;
            en.Bairro = enm.Bairro;
            en.Cidade = enm.Cidade;
            en.Numero = enm.Numero;
            en.Complemento = enm.Complemento;
            en.CodigoPostal = enm.CodigoPostal;
            en.Estado = enm.Estado;
            en.Pais = enm.Pais;
            en.IsMatriz = true;

            if (en.Id != default(int)) { _empresaService.Update(en); }
            else { _empresaService.Insert(en); }


            IList<Grupo> grupos = _grupoService.GetAllGrupo(User.Identity.Name);

            IndexModel im = new IndexModel();
            im.SelectedGroupId = grupos.FirstOrDefault().Id;

            im.Grupos = grupos;
            return View("Index", im);
        }
        
        
    }
}