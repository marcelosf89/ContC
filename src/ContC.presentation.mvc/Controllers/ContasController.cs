using ContC.CorssCutting.Exceptions;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Config;
using ContC.presentation.mvc.Extension;
using ContC.presentation.mvc.Models.ContasModels;
using ContC.presentation.mvc.Models.ExceptionModels;
using ContC.Repositories.Mapping.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class ContasController : ControllerException
    {
        private IBoletoService _boletoService;
        private ICategoriaService _icategoriaServicos;
        private IFornecedorService _fornecedorService;
        private IUsuarioService _userService;

        public ContasController(IBoletoService boletoService, ICategoriaService icategoriaServicos, IFornecedorService fornecedorService, IUsuarioService userService)
        {
            _boletoService = boletoService;
            _icategoriaServicos = icategoriaServicos;
            _fornecedorService = fornecedorService;
            _userService = userService;
        }

        // GET: Contas
        public ActionResult Index(int empresaId)
        {
            return View(empresaId);
        }

        public ActionResult Nova(int empresaId)
        {
            ContasManterModel cmm = new ContasManterModel();
            cmm.EmpresaId = empresaId;
            cmm.TempId = Guid.NewGuid();
            cmm.Categorias = _icategoriaServicos.GetByEmpresa(empresaId).Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
            return View("Manter", cmm);
        }

        public ActionResult Dashboard(int empresaId)
        {
            IList<ContasDTO> li = _boletoService.GetContasByDashboard(empresaId, 15);

            return View(li);
        }

        public ActionResult ModalPagar(int boletoId)
        {
            Boleto boleto = _boletoService.Find(boletoId);

            return View(boleto);
        }

        public ActionResult ModalDetalhe(int boletoId)
        {
            Boleto boleto = _boletoService.Find(boletoId);

            return View(boleto);
        }

        [HttpPost]
        public void UploadFile(HttpPostedFileBase file, int boletoId)
        {
            if (Request.Files.Count < 1)
            {
                return;
            }
            // Verify that the user selected a file
            HttpPostedFileBase hpfw = Request.Files[0];
            if (hpfw != null && hpfw.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(hpfw.FileName);
                // TODO: need to define destination
                var path = Path.Combine(ConfigurationFactory.Instance.PastaComprovante, boletoId + ".pdf");
                hpfw.SaveAs(path);
            }
        }
        [HttpPost]
        public void UploadFileBoleto(HttpPostedFileBase file, string tempId)
        {
            if (Request.Files.Count < 1)
            {
                return;
            }
            // Verify that the user selected a file
            HttpPostedFileBase hpfw = Request.Files[0];
            if (hpfw != null && hpfw.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(hpfw.FileName);
                // TODO: need to define destination
                var path = Path.Combine(ConfigurationFactory.Instance.PastaTemp, tempId + ".pdf");
                hpfw.SaveAs(path);
            }
        }

        [HttpPost]
        public void RemoveUploadFileBoleto(string tempId)
        {
            System.IO.File.Delete(Path.Combine(ConfigurationFactory.Instance.PastaTemp, tempId + ".pdf"));
        }

        public ActionResult Pagar(Boleto boleto)
        {
            if (!System.IO.File.Exists(Path.Combine(ConfigurationFactory.Instance.PastaComprovante, boleto.Id + ".pdf")))
            {
                return View("Error");
            }

            boleto = _boletoService.Find(boleto.Id);
            boleto.DataPagamento = DateTime.Now;
            _boletoService.Update(boleto);

            return View();
        }

        public JsonResult GetFornecedor(int maxRows, string startsWith, int empresaId, int categoriaId)
        {
            IList<Fornecedor> lfor = _fornecedorService.GetAllByEmpresaCategoria(startsWith, empresaId, categoriaId, maxRows);
            return Json(lfor, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Agendar(ContasManterModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categorias = _icategoriaServicos.GetByEmpresa(model.EmpresaId).Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                return View("Manter", model);
            }
            try
            {

                Boleto b = new Boleto();
                b.DataVencimento = model.Validade.Value;
                b.Empresa = new Empresa() { Id = model.EmpresaId };
                b.Fornecedor = new Fornecedor() { Id = model.FornecedorId };
                b.Numero = model.NumeroConta.Replace(".", "").Replace(" ", "");
                b.Valor = model.Valor;

                if (!model.UploadFile)
                {
                    throw new Exception("O Arquivo não foi carregado");
                }
                FileInfo fi = new FileInfo(Path.Combine(ConfigurationFactory.Instance.PastaTemp, model.TempId + ".pdf"));



                try
                {
                    UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
                    _boletoService.Insert(b, fi, ConfigurationFactory.Instance.PastaBoleto);
                    UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
                }
                catch (ExceptionMessage em)
                {
                    UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                    throw em;
                }
                catch (Exception ex)
                {
                    UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                    throw new StatusException("Erro interno . Favor informe ao administrador.");
                }
                
            }
            catch (Exception ex)
            {
                model.Erro = ex.Message;
                model.Categorias = _icategoriaServicos.GetByEmpresa(model.EmpresaId).Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                return View("Manter", model);
            }

            return View("Index", model.EmpresaId);
        }

        public ActionResult FiltrarConsulta(int empresaId, string dataInicio, string dataFinal)
        {
            IList<ContasDTO> boletos = _boletoService.GetContasByEmpresaPeriodo(empresaId,
                Convert.ToDateTime(dataInicio), Convert.ToDateTime(dataFinal));

            return View(boletos);
        }

        public ActionResult ModalRemover(int boletoId)
        {
            Boleto boleto = _boletoService.Find(boletoId);

            return View(boleto);
        }


        public void Remover(Boleto model)
        {
            try
            {
                Boleto boleto = _boletoService.Find(model.Id);
                boleto.MotivoCancelamento = model.MotivoCancelamento;
                boleto.UsuarioCancelamento = _userService.GetUsuario(User.Identity.Name);
                _boletoService.Remove(boleto);
            }
            catch (Exception ex)
            {
                throw new StatusException(ex.Message);
            }
        }

        public String GetDateBoleto(string numeroDias)
        {
            try
            {
                DateTime dt = Convert.ToDateTime("07/10/1997", new CultureInfo("pt-BR"));
                dt = dt.AddDays(Convert.ToInt32(numeroDias));
                return dt.ToShortDateString();
            }
            catch (Exception) { return ""; }

        }

        public ActionResult GetFile(int boletoId)
        {
            String sourceFileName = "comprovante_boleto_" + boletoId.ToString("00000") + ".pdf";
            string fp = Path.Combine(ConfigurationFactory.Instance.PastaComprovante, boletoId + ".pdf");
            byte[] b = System.IO.File.ReadAllBytes(fp);
            HttpContext.Response.BinaryWrite(b);
            return File(b, "text/octet-stream", sourceFileName);
        }



    }
}