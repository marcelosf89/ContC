using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Models.ReceitaModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class ReceitasController : Controller
    {
        IEmpresaService _empresaService;
        IReceitaService _receitaService;

        public ReceitasController(IEmpresaService empresaService, IReceitaService receitaService)
        {
            _empresaService = empresaService;
            _receitaService = receitaService;
        }
        // GET: Receitas
        public ActionResult Index(int empresaId)
        {
            Empresa emp = _empresaService.Find(empresaId);
            ReceitaEmpresaModel rem = new ReceitaEmpresaModel()
            {
                EmpresaId = empresaId,
                AtulizarReceitas = emp.TemExtensaoReceita,
                RabbitMqQueue = emp.RabbitmqQueue
            };
            return View(rem);
        }

        public string AtualizarReceitas(int empresaId)
        {
            _empresaService.SendCommunicationReceita(empresaId);
            return "Comunicação Enviada para a extensão";

        }

        public ActionResult FiltrarConsulta(int empresaId, string dataInicio, string horaInicial, string dataFinal, string horaFinal)
        {
            DateTime hora = Convert.ToDateTime(horaInicial, CultureInfo.CurrentCulture);

            DateTime inicio = Convert.ToDateTime(dataInicio);
            inicio = inicio.AddHours(hora.Hour); inicio = inicio.AddMinutes(hora.Minute); inicio = inicio.AddSeconds(0);

            hora = Convert.ToDateTime(horaFinal, CultureInfo.CurrentCulture);
            DateTime final = Convert.ToDateTime(dataFinal);
            final = final.AddHours(hora.Hour); final = final.AddMinutes(hora.Minute); final = final.AddSeconds(59);

            IList<ReceitasDTO> receitas = _receitaService.GetReceitasByEmpresaPeriodo(
                empresaId, inicio, final);

            return View(receitas);
        }

        public ActionResult Chart(int empresaId, string dataInicio, string horaInicial, string dataFinal, string horaFinal)
        {
            DateTime hora = Convert.ToDateTime(horaInicial, CultureInfo.CurrentCulture);

            DateTime inicio = Convert.ToDateTime(dataInicio);
            inicio = inicio.AddHours(hora.Hour); inicio = inicio.AddMinutes(hora.Minute); inicio = inicio.AddSeconds(0);

            hora = Convert.ToDateTime(horaFinal, CultureInfo.CurrentCulture);
            DateTime final = Convert.ToDateTime(dataFinal);
            final = final.AddHours(hora.Hour); final = final.AddMinutes(hora.Minute); final = final.AddSeconds(59);

            IList<ReceitasDataChartDTO> receitas = _receitaService.GetReceitasDataChartByEmpresaPeriodo(
                empresaId, inicio, final);

            DateTime _jan1st1970 = new DateTime(1970, 1, 1);
            receitas = receitas.OrderBy(p => p.Data).ToList();

            object[] obj =  receitas.Select(p => new[] {
                Convert.ToInt64((p.Data - _jan1st1970).TotalMilliseconds), 
                Convert.ToInt32(p.Valor)
            }).ToArray();

            return View(obj);

        }
    }
}