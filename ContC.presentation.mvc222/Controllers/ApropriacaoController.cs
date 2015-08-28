using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContC.domain.entities.Context;
using ContC.domain.entities.Models;
using ContC.domain.services;
using ContC.presentation.mvc.Models;
using Microsoft.Ajax.Utilities;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System.Web.Mvc;
using ContC.domain.entities.DTO;
using DevExpress.Web.ASPxHtmlEditor.Internal;
using DevExpress.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class ApropriacaoController : Controller
    {
        // GET: Apropriacao
        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult Index()
        {
            ViewBag.Title = "Apropriação de Horas";

            var empresaViewModel = PreencherEmpresaViewModel(User.Identity.Name);

            return View(empresaViewModel);
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        // GET: ApropriacaoPartial/{0}
        public ActionResult ApropriacaoPartial(string empresaId)
        {
            ViewBag.Title = "Apropriação de Horas";
            var id = Convert.ToInt32(empresaId);

            var apropModel = PreencheApropriacaoViewModel(User.Identity.Name, id);

            return PartialView(apropModel);
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        [HttpPost]
        public JsonResult IncluirAtividade(ApropriacaoViewModel model)
        {
            MensagemViewModel mensagem;
            try
            {
                using (IDataContextAsync context = new DbContext())
                using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
                {
                    //Valida preenchimento de campos obrigatórios
                    ValidarApropriacao(model);

                    //Hidrata variáveis
                    var empresaId = Convert.ToInt32(model.EmpresaId);
                    var competenciaId = Convert.ToInt32(model.CompetenciaId);
                    var localidadeId = Convert.ToInt32(model.LocalidadeId);
                    var centroDeCustoId = Convert.ToInt32(model.CentroDeCustoId);
                    var categoriaId = Convert.ToInt32(model.CategoriaId);

                    //obtem serviços necessários
                    IRepositoryAsync<Usuario> userRepository = new Repository<Usuario>(context, unitOfWork);
                    UsuarioService userServiceService = new UsuarioService(userRepository);
                    IRepositoryAsync<Empresa> empresaRepository = new Repository<Empresa>(context, unitOfWork);
                    EmpresaService empresaService = new EmpresaService(empresaRepository);
                    IRepositoryAsync<Relacao> relacaoRepository = new Repository<Relacao>(context, unitOfWork);
                    RelacaoService relacaoService = new RelacaoService(relacaoRepository);
                    IRepositoryAsync<Competencia> competenciaRepository = new Repository<Competencia>(context, unitOfWork);
                    CompetenciaService competenciaService = new CompetenciaService(competenciaRepository);
                    IRepositoryAsync<Localidade> localidadeRepository = new Repository<Localidade>(context, unitOfWork);
                    LocalidadeService localidadeService = new LocalidadeService(localidadeRepository);
                    IRepositoryAsync<CentroDeCusto> centroDeCustoRepository = new Repository<CentroDeCusto>(context, unitOfWork);
                    CentroDeCustoService centroDeCustoService = new CentroDeCustoService(centroDeCustoRepository);
                    IRepositoryAsync<Categoria> categoriaRepository = new Repository<Categoria>(context, unitOfWork);
                    CategoriaService categoriaService = new CategoriaService(categoriaRepository);
                    IRepositoryAsync<Apontamento> apontamentoRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apontamentoService = new ApontamentoService(apontamentoRepository);

                    //obtem entidades necessárias
                    var usuario = userServiceService.Query(s => s.Email == User.Identity.Name).Select().First();
                    var empresa = empresaService.Query(s => s.Id == empresaId).Select().First();
                    var relacao = relacaoService.ObterRelacaoUsuarioEmpresa(usuario, empresa);
                    var competencia = competenciaService.Query(s => s.Id == competenciaId).Select().First();
                    var localidade = localidadeService.Query(s => s.Id == localidadeId).Select().First();
                    var centroDeCusto = centroDeCustoService.Query(s => s.Id == centroDeCustoId).Select().First();
                    var categoria = categoriaService.Query(s => s.Id == categoriaId).Select().First();
                    var nacional = localidadeService.Query(s => s.Sigla == "BR").Select().FirstOrDefault();

                    //se não houver nenhuma localidade de ambito nacional, então gere uma exceção
                    if (nacional == null)
                        throw new Exception("Nenhuma localidade de ambito Nacional foi localizada, contacte o administrador e informe que é obrigatório que exista uma localidade com a sigla [BR] e a descrição [Nacional] cadastrada no sistema.");

                    //valida as datas em relação as competências
                    if (model.DataInicial != null) competenciaService.ValidarDataCompetencia(competenciaId, model.DataInicial.Value);
                    if (model.DataFinal != null) competenciaService.ValidarDataCompetencia(competenciaId, model.DataFinal.Value);

                    //verifica se o Hh Diário é superior ao limite da relação
                    if (model.HhDiario > relacao.QtdeMaximaHorasDiarias)
                        throw new Exception(string.Format("O <b>Hh diário</b> informado de {0} horas é maior do que o limite de {1} horas que foi configurado na relação deste Usuário nesta Empresa.", model.HhDiario, relacao.QtdeMaximaHorasDiarias));

                    var dataInicialInvalida = !(model.DataInicial != null &&
                                               (model.DataInicial >= usuario.DataInicio &&
                                                 model.DataInicial <=
                                                 usuario.DataTermino.GetValueOrDefault(model.DataInicial.Value)));
                    
                    //verifica se o usuário não está bloqueado - Data Inicial tem estar num período válido de atividades do usuário
                    if (dataInicialInvalida)
                    {
                        throw new Exception(string.Format("O <b>Usuário [{0}]</b> não está mais disponível para registro de atividades nesta Data Inicial informada.", usuario.Nome));
                    }

                    var dataFinalInvalida = !(model.DataFinal != null &&
                                             (model.DataFinal >= usuario.DataInicio &&
                                               model.DataFinal <=
                                               usuario.DataTermino.GetValueOrDefault(model.DataFinal.Value)));
                    //verifica se o usuário não está bloqueado - Data Final tem estar num período válido de atividades do usuário
                    if (dataFinalInvalida)
                    {
                        throw new Exception(string.Format("O <b>Usuário [{0}]</b> não está mais disponível para registro de atividades nesta Data Final informada.", usuario.Nome));
                    }

                    //verifica se a competência não está bloqueada
                    if (!competencia.Status)
                    {
                        throw new Exception(string.Format("A <b>Competência [{0}]</b> não está mais disponível para registro de atividades.", competencia.Descricao));
                    }

                    //verifica se o centro de custo não está bloqueado
                    if (!centroDeCusto.Situacao)
                    {
                        throw new Exception(string.Format("O <b>Centro de Custo [{0}]</b> não está mais disponível para registro de atividades.", centroDeCusto.Descricao));
                    }

                    //Salva as atividades no banco de dados
                    try
                    {
                        //inicia uma transação
                        unitOfWork.BeginTransaction();
                        //registra as atividades
                        apontamentoService.RegistrarAtividades(usuario, empresa, competencia, localidade, centroDeCusto, categoria, relacao, model.DataInicial.Value, model.DataFinal.Value, model.HhDiario, model.Descricao, model.IncluirFeriados, model.IncluirFinaisDeSemana, model.AprovarAutomaticamente, nacional);
                        //propaga e envia os comando de modificação ao banco de dados
                        var saveChangesAsync = unitOfWork.SaveChanges();
                        //Persiste definitivamente as informaçãos no banco de dados
                        unitOfWork.Commit();
                    }
                    catch (Exception)
                    {
                        //Caso exista uma exceção, cancela as alterações
                        unitOfWork.Rollback();
                        //propaga a exceção para frente
                        throw;
                    }

                    //monta a mensagem de retorno com sucesso.
                    mensagem = new MensagemViewModel { Tipo = "0", Texto = "O processo de registro de atividades foi concluído com sucesso." };

                }
            }
            catch (Exception e)
            {
                //monta a mensagem de retorno com erro.
                mensagem = new MensagemViewModel { Tipo = "1", Texto = e.Message };
            }
            //Retorna um Json com a mensagem de retorno.
            return Json(mensagem, "json");
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        [HttpPost]
        public ActionResult ApropriacaoGridDeletePartial(int id)
        {
            int empresaId = 0;
            int competenciaId = 0;
            if (id >= 0)
            {
                using (IDataContextAsync context = new DbContext())
                using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    var apontamento = apService.Query(a => a.Id == id).Select().FirstOrDefault();

                    if (apontamento != null)
                    {
                        empresaId = apontamento.Empresa.Id;
                        competenciaId = apontamento.Competencia.Id;
                    }

                    try
                    {
                        unitOfWork.BeginTransaction();
                        apService.Delete(apontamento);
                        unitOfWork.SaveChanges();
                        unitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                        unitOfWork.Rollback();
                    }
                }
            }
            return PartialView("ApropriacaoGridPartial", PreencherApontamentoDTO(empresaId.ToString(), competenciaId.ToString()));
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult ApropriacaoGridPartial(string empresaId, string competenciaId)
        {
            return PartialView(PreencherApontamentoDTO(empresaId, competenciaId));
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult CapturaPartial(int id)
        {
            ApropriacaoViewModel apropModel = PreencheApropriacaoViewModel(id);
            return PartialView("ApropriacaoPartial", apropModel);
        }

        private IList<ApontamentoDTO> PreencherApontamentoDTO(string empresaId, string competenciaId)
        {

            IList<ApontamentoDTO> apontamentos = null;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                //Empresa
                int empId = Convert.ToInt32(empresaId);
                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);
                var empresa = eService.Query(e => e.Id == empId).Select().FirstOrDefault() ?? new Empresa();

                //Usuario
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario usuario = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();
                //Competencia
                int compId = Convert.ToInt32(competenciaId);
                IRepositoryAsync<Competencia> compRepository = new Repository<Competencia>(context, unitOfWork);
                CompetenciaService compService = new CompetenciaService(compRepository);
                var competencia = compService.Query(c => c.Id == compId).Select().FirstOrDefault() ?? new Competencia();
                //Lista de Apontamentos
                IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                ApontamentoService apService = new ApontamentoService(apRepository);
                apontamentos = apService.ObterApontamentosDTOPorUsuarioEmpresaCompetencia(usuario, empresa, competencia, ListProvider.GetNacional(context, unitOfWork));
            }
            return apontamentos;
        }

        private EmpresaViewModel PreencherEmpresaViewModel(string username)
        {
            EmpresaViewModel empresaViewModel = new EmpresaViewModel();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);

                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario usuario = uService.Query(c => c.Email == username).Select().FirstOrDefault();

                //Empresas e Cabeçalho
                empresaViewModel.Profissional = usuario.Nome;
                empresaViewModel.Lider = usuario.Lider.Nome;
                var empresasPorUsuario = eService.ObterEmpresaPorUsuario(usuario);
                IEnumerable<SelectListItem> empresas = empresasPorUsuario.Select(x => new SelectListItem() { Text = x.RazaoSocial, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)empresas).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                empresaViewModel.Empresas = new SelectList(empresas, "Value", "Text", "0");

            }

            return empresaViewModel;

        }

        private ApropriacaoViewModel PreencheApropriacaoViewModel(string username, int empresaId)
        {
            ApropriacaoViewModel apropModel = new ApropriacaoViewModel();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);

                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario usuario = uService.Query(c => c.Email == username).Select().First();

                apropModel.HabilitaAprovarAutomaticamente = (usuario.Id == usuario.Lider.Id);
                apropModel.AprovarAutomaticamente = false;
                apropModel.IncluirFinaisDeSemana = false;
                apropModel.IncluirFeriados = false;

                //Empresas e Cabeçalho
                Empresa empresa = eService.Query(c => c.Id == empresaId).Select().FirstOrDefault();
                apropModel.EmpresaId = empresaId.ToString();

                if (empresa != null)
                {
                    //Competencia
                    IRepositoryAsync<Competencia> compRepository = new Repository<Competencia>(context, unitOfWork);
                    CompetenciaService compService = new CompetenciaService(compRepository);
                    var q = compService.Query(c => c.Status == true).Select().OrderByDescending(o => o.DataFinal);
                    IEnumerable<SelectListItem> competencias =
                        q.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)competencias).Insert(0,
                        new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                    apropModel.Competencias = competencias;

                    //Centro de Custos
                    IRepositoryAsync<CentroDeCusto> ccRepository = new Repository<CentroDeCusto>(context, unitOfWork);
                    CentroDeCustoService ccService = new CentroDeCustoService(ccRepository);
                    var cc = ccService.ObterCentroDeCustosPorUsuarioEmpresa(usuario, empresa);
                    apropModel.CentroDeCustos =
                        cc.Where(x => x.Situacao == true).Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)apropModel.CentroDeCustos).Insert(0,
                        new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    //Categorias
                    IRepositoryAsync<Categoria> ctRepository = new Repository<Categoria>(context, unitOfWork);
                    CategoriaService ctService = new CategoriaService(ctRepository);
                    IRepositoryAsync<TipoRelacao> trRepository = new Repository<TipoRelacao>(context, unitOfWork);
                    TipoRelacaoService trService = new TipoRelacaoService(trRepository);
                    var tpRelacao = trService.ObterTipoDeRelacaoUsuarioEmpresa(usuario, empresa);

                    if (tpRelacao != null)
                    {
                        var categorias = ctService.ObterCategoriasPorEmpresaTipoRelacaoDoUsuario(tpRelacao, empresa, usuario);
                        apropModel.Categorias = categorias.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();                       
                    }
                    else
                    {
                       apropModel.Categorias = new List<SelectListItem>();
                    }
                    ((List<SelectListItem>)apropModel.Categorias).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    //Localidades
                    IRepositoryAsync<Localidade> lcRepository = new Repository<Localidade>(context, unitOfWork);
                    LocalidadeService lcService = new LocalidadeService(lcRepository);
                    var localidades = lcService.Query().Select().ToList();
                    apropModel.Localidades =
                        localidades.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() })
                            .ToList();
                    ((List<SelectListItem>)apropModel.Localidades).Insert(0,
                        new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    //Lista de Apontamentos
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    var apontamentos = apService.ObterApontamentosDTOPorUsuarioEmpresaCompetencia(usuario, empresa, new Competencia(), ListProvider.GetNacional(context, unitOfWork));
                    apropModel.Apontamentos = apontamentos;

                }

            }

            return apropModel;
        }

        private ApropriacaoViewModel PreencheApropriacaoViewModel(int apontamentoId)
        {
            ApropriacaoViewModel apropModel = new ApropriacaoViewModel();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                //Lista de Apontamentos
                IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                ApontamentoService apService = new ApontamentoService(apRepository);
                var apontamento = apService.Query(a => a.Id == apontamentoId).Select().FirstOrDefault();

                if (apontamento != null)
                {

                    apropModel.HabilitaAprovarAutomaticamente = (apontamento.Usuario.Id == apontamento.Usuario.Lider.Id);
                    apropModel.AprovarAutomaticamente = false;
                    apropModel.IncluirFinaisDeSemana = false;
                    apropModel.IncluirFeriados = false;

                    //Competencia
                    IRepositoryAsync<Competencia> compRepository = new Repository<Competencia>(context, unitOfWork);
                    CompetenciaService compService = new CompetenciaService(compRepository);
                    var q = compService.Query(c => c.Status == true).Select().OrderByDescending(o => o.DataFinal);
                    IEnumerable<SelectListItem> competencias =
                        q.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)competencias).Insert(0,
                        new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                    apropModel.Competencias = competencias;
                    var selectedCompetencia = competencias.First(x => x.Value == apontamento.Competencia.Id.ToString());
                    selectedCompetencia.Selected = true;

                    //Centro de Custos
                    IRepositoryAsync<CentroDeCusto> ccRepository = new Repository<CentroDeCusto>(context, unitOfWork);
                    CentroDeCustoService ccService = new CentroDeCustoService(ccRepository);
                    var cc = ccService.ObterCentroDeCustosPorUsuarioEmpresa(apontamento.Usuario, apontamento.Empresa);
                    apropModel.CentroDeCustos =
                        cc.Where(x => x.Situacao == true).Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)apropModel.CentroDeCustos).Insert(0,
                        new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                    var selectedCentroDeCusto = apropModel.CentroDeCustos.FirstOrDefault(x => x.Value == apontamento.CentroDeCusto.Id.ToString()) ?? apropModel.CentroDeCustos.First(x => x.Value == "0");
                    selectedCentroDeCusto.Selected = true;

                    //Categorias
                    IRepositoryAsync<Categoria> ctRepository = new Repository<Categoria>(context, unitOfWork);
                    CategoriaService ctService = new CategoriaService(ctRepository);
                    IRepositoryAsync<TipoRelacao> trRepository = new Repository<TipoRelacao>(context, unitOfWork);
                    TipoRelacaoService trService = new TipoRelacaoService(trRepository);
                    var tpRelacao = trService.ObterTipoDeRelacaoUsuarioEmpresa(apontamento.Usuario, apontamento.Empresa);
                    if (tpRelacao != null)
                    {
                        var categorias = ctService.ObterCategoriasPorEmpresaTipoRelacaoDoUsuario(tpRelacao,
                            apontamento.Empresa, apontamento.Usuario);
                        apropModel.Categorias =
                            categorias.Select(x => new SelectListItem() {Text = x.Descricao, Value = x.Id.ToString()})
                                .ToList();
                    }
                    else
                    {
                        apropModel.Categorias = new List<SelectListItem>();
                    }
                    ((List<SelectListItem>)apropModel.Categorias).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                    var selectedCategoria = apropModel.Categorias.First(x => x.Value == apontamento.Categoria.Id.ToString());
                    selectedCategoria.Selected = true;

                    //Localidades
                    IRepositoryAsync<Localidade> lcRepository = new Repository<Localidade>(context, unitOfWork);
                    LocalidadeService lcService = new LocalidadeService(lcRepository);
                    var localidades = lcService.Query().Select().ToList();
                    apropModel.Localidades =
                        localidades.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() })
                            .ToList();
                    ((List<SelectListItem>)apropModel.Localidades).Insert(0,
                        new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                    var selectedLocalidade = apropModel.Localidades.First(x => x.Value == apontamento.Localidade.Id.ToString());
                    selectedLocalidade.Selected = true;

                    apropModel.DataInicial = apontamento.Data;
                    apropModel.DataFinal = apontamento.Data;
                    apropModel.Descricao = apontamento.Descricao;
                    apropModel.HhDiario = apontamento.Hh;
                    apropModel.EmpresaId = apontamento.Empresa.Id.ToString();
                    apropModel.Apontamentos = apService.ObterApontamentosDTOPorUsuarioEmpresaCompetencia(apontamento.Usuario, apontamento.Empresa, apontamento.Competencia, ListProvider.GetNacional(context, unitOfWork));
                }

            }

            return apropModel;
        }

        private void ValidarApropriacao(ApropriacaoViewModel model)
        {
            if (Convert.ToInt32(!string.IsNullOrEmpty(model.EmpresaId) ? model.EmpresaId : "0") == 0)
            {
                throw new Exception("Favor informar a <b>Empresa</b> onde será apropriada a atividade.");
            }
            if (Convert.ToInt32(!string.IsNullOrEmpty(model.CompetenciaId) ? model.CompetenciaId : "0") == 0)
            {
                throw new Exception("Favor informar a <b>Competência</b> onde será apropriada a atividade.");
            }

            if (Convert.ToInt32(!string.IsNullOrEmpty(model.LocalidadeId) ? model.LocalidadeId : "0") == 0)
            {
                throw new Exception("Favor informar a <b>Localidade</b> onde será apropriada a atividade.");
            }
            if (model.DataInicial == null)
            {
                throw new Exception("Favor informar a <b>Data inicial</b> da apropriação da atividade.");
            }
            if (model.DataFinal == null)
            {
                throw new Exception("Favor informar a <b>Data final</b> da apropriação da atividade.");
            }
            var dtInicial = Convert.ToDateTime(model.DataInicial);
            var dtFinal = Convert.ToDateTime(model.DataFinal);
            if (dtInicial > dtFinal)
            {
                throw new Exception("A <b>Data final</b> da apropriação dever ser maior ou igual a <b>Data inicial</b>.");
            }
            if (Convert.ToInt32(!string.IsNullOrEmpty(model.CentroDeCustoId) ? model.CentroDeCustoId : "0") == 0)
            {
                throw new Exception("Favor informar o <b>Centro de custo</b> onde será apropriada a atividade.");
            }
            if (Convert.ToInt32(!string.IsNullOrEmpty(model.CategoriaId) ? model.CategoriaId : "0") == 0)
            {
                throw new Exception("Favor informar a <b>Categoria</b> onde será apropriada a atividade.");
            }
            if (string.IsNullOrEmpty(model.Descricao) || string.IsNullOrWhiteSpace(model.Descricao))
            {
                throw new Exception("Favor informar a <b>Descrição</b> da atividade.");
            }
            if (model.HhDiario <= 0)
            {
                throw new Exception("Favor informar o <b>Hh diário</b> da atividade.");
            }
            if ((model.HhDiario - Math.Truncate(model.HhDiario) != 0) && (model.HhDiario - Math.Truncate(model.HhDiario) != (decimal)0.50))
            {
                throw new Exception("A quantidade decimal de <b>Hh diário</b> da atividade deve ser .00(hora inteira) ou .50(meia-hora).");
            }


        }

    }
}