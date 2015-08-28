using System;
using System.Collections.Generic;
using System.Linq;
using ContC.domain.entities.Context;
using ContC.domain.entities.Models;
using ContC.domain.services;
using ContC.presentation.mvc.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace ContC.presentation.mvc.Controllers
{
    public class ListProvider
    {
        public static IList<Competencia> GetCompetencias()
        {
            IList<Competencia> competencias;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Competencia> competenciaRepository = new Repository<Competencia>(context, unitOfWork);
                CompetenciaService competenciaService = new CompetenciaService(competenciaRepository);
                competencias = competenciaService.Query().Select().OrderByDescending(o => o.DataFinal).ToList();
            }
            return competencias;
        }

        public static IList<TipoCategoria> GetTiposCategoria()
        {
            IList<TipoCategoria> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<TipoCategoria> repository = new Repository<TipoCategoria>(context, unitOfWork);
                TipoCategoriaService service = new TipoCategoriaService(repository);
                lista = service.Query().Select().ToList();
            }
            return lista;
        }

        public static IList<Categoria> GetCategorias()
        {
            IList<Categoria> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Categoria> repository = new Repository<Categoria>(context, unitOfWork);
                CategoriaService service = new CategoriaService(repository);
                lista = service.Query().Select().ToList();
            }
            return lista;
        }

        public static IList<Empresa> GetEmpresas()
        {
            IList<Empresa> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Empresa> repository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService service = new EmpresaService(repository);
                lista = service.Query().Select().ToList();
            }
            return lista;
        }

        public static IList<Usuario> GetUsuarios()
        {
            IList<Usuario> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Usuario> repository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService service = new UsuarioService(repository);
                lista = service.Query().Select().OrderBy(o => o.Nome).ToList();
            }
            return lista;
        }

        public static IList<Localidade> GetLocalidades()
        {
            IList<Localidade> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Localidade> repository = new Repository<Localidade>(context, unitOfWork);
                LocalidadeService service = new LocalidadeService(repository);
                lista = service.Query().Select().OrderBy(o => o.Sigla).ToList();
            }
            return lista;
        }

        public static Localidade GetNacional(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            IRepositoryAsync<Localidade> repository = new Repository<Localidade>(context, unitOfWork);
            var service = new LocalidadeService(repository);
            var nacional = service.Query(s => s.Sigla.ToUpper() == "BR").Select().FirstOrDefault();
            return nacional;
        }

        public static IList<TipoRelacao> GetTiposRelacao()
        {
            IList<TipoRelacao> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<TipoRelacao> repository = new Repository<TipoRelacao>(context, unitOfWork);
                TipoRelacaoService service = new TipoRelacaoService(repository);
                lista = service.Query().Select().OrderBy(o => o.Descricao).ToList();
            }
            return lista;
        }

        public static IList<CategoriaViewModel> GetCategoriasViewModel()
        {
            IList<CategoriaViewModel> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Categoria> repository = new Repository<Categoria>(context, unitOfWork);
                CategoriaService service = new CategoriaService(repository);
                lista = service.Query().Select().ToList()
                    .Select(s => new CategoriaViewModel
                    {
                        Id = s.Id,
                        TipoCategoriaId = s.TipoCategoria.Id,
                        Descricao = s.Descricao,
                        TiposRelacaoIds = GetListaDeIdsDeTipoRelacaoPorCategoria(s),
                        TiposRelacaoDescritivos = GetListaDeDescritivosDeTipoRelacaoPorCategoria(s)
                    }).ToList();

            }
            return lista;
        }

        public static IList<CategoriaViewModel> GetCategoriasViewModelPorEmpresa(int empresaId)
        {
            IList<CategoriaViewModel> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Categoria> repository = new Repository<Categoria>(context, unitOfWork);
                CategoriaService service = new CategoriaService(repository);
                lista = service.Query(e => e.Empresa.Id == empresaId).Select().ToList()
                    .Select(s => new CategoriaViewModel
                    {
                        Id = s.Id,
                        TipoCategoriaId = s.TipoCategoria.Id,
                        Descricao = s.Descricao,
                        TiposRelacaoIds = GetListaDeIdsDeTipoRelacaoPorCategoria(s),
                        TiposRelacaoDescritivos = GetListaDeDescritivosDeTipoRelacaoPorCategoria(s)
                    }).ToList();

            }
            return lista;
        }

        public static IList<TipoRelacao> GetTiposRelacaoFromListaDeIds(string listaIds, IList<TipoRelacao> listaTipoRelacao)
        {
            var strIds = listaIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] intIds = new int[strIds.Length];
            for (int i = 0; i < strIds.Length; i++)
            {
                intIds[i] = int.Parse(strIds[i]);
            }
            IList<TipoRelacao> lista = listaTipoRelacao.Where(x => intIds.Contains(x.Id)).OrderBy(o => o.Descricao).ToList();
            return lista;
        }

        private static IList<TipoRelacaoCategoriaViewModel> GetTiposRelacaoCategoriasViewModelPorCategoria(Categoria s)
        {
            var tiposRelacao = s.TiposRelacao;
            return tiposRelacao.Select(t => new TipoRelacaoCategoriaViewModel { Id = string.Format("{0}, {1}", s.Id, t.Id), CategoriaId = s.Id, TipoRelacaoId = t.Id, CategoriaDescricao = s.Descricao, TipoRelacaoDescricao = t.Descricao }).ToList();
        }

        public static string GetListaDeIdsDeTipoRelacaoPorCategoria(Categoria cat)
        {
            return string.Join(",", cat.TiposRelacao.Select(x => x.Id).ToArray());
        }

        public static string GetListaDeDescritivosDeTipoRelacaoPorCategoria(Categoria cat)
        {
            return string.Join(",", cat.TiposRelacao.Select(x => x.Descricao).ToArray());
        }

        public static IList<TipoRelacaoCategoriaViewModel> GetTiposRelacaoCategoriasViewModel()
        {
            List<TipoRelacaoCategoriaViewModel> lista = new List<TipoRelacaoCategoriaViewModel>();
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Categoria> repository = new Repository<Categoria>(context, unitOfWork);
                CategoriaService service = new CategoriaService(repository);
                List<Categoria> listaCategoria = service.Query().Select().ToList();
                foreach (var cat in listaCategoria)
                {
                    lista.AddRange(GetTiposRelacaoCategoriasViewModelPorCategoria(cat));
                }
            }
            return lista;
        }

        public static IList<CentroDeCustoViewModel> GetCentroDeCustosViewModel()
        {
            IList<CentroDeCustoViewModel> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<CentroDeCusto> repository = new Repository<CentroDeCusto>(context, unitOfWork);
                CentroDeCustoService service = new CentroDeCustoService(repository);
                lista = service.Query().Select().ToList().OrderBy(o => o.Empresa.RazaoSocial).Select(s => new CentroDeCustoViewModel { Id = s.Id, EmpresaId = s.Empresa.Id, Descricao = s.Descricao, Sigla = s.Sigla, Situacao = s.Situacao, Tipo = s.Tipo }).ToList();
            }
            return lista;
        }

        public static IList<TipoCentroDeCustoViewModel> GetTiposCentroDeCustoViewModel()
        {
            var lista = new List<TipoCentroDeCustoViewModel>
            {
                new TipoCentroDeCustoViewModel {Sigla = "�rea", Descricao = "�rea"},
                new TipoCentroDeCustoViewModel {Sigla = "Contrato", Descricao = "Contrato"},
                new TipoCentroDeCustoViewModel {Sigla = "Projeto", Descricao = "Projeto"}
            };
            return lista;
        }

        public static IList<RelacaoViewModel> GetRelacoesViewModelPorTipo(int tipoRelacaoId)
        {
            IList<RelacaoViewModel> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Relacao> repository = new Repository<Relacao>(context, unitOfWork);
                RelacaoService service = new RelacaoService(repository);
                lista = service.Query(s => s.TipoRelacao.Id == tipoRelacaoId).Select().ToList().OrderBy(o => o.Empresa.RazaoSocial).ThenBy(n => n.Usuario.Nome).Select(s => new RelacaoViewModel { Id = s.Id, EmpresaId = s.Empresa.Id, TipoRelacaoId = s.TipoRelacao.Id, UsuarioId = s.Usuario.Id, QtdeMaximaHorasDiarias = s.QtdeMaximaHorasDiarias, RazaoSocialFornecedor = s.RazaoSocial }).ToList();
            }
            return lista;
        }

        public static IList<FeriadoViewModel> GetFeriadosViewModelPorLocalidade(int localidadeId)
        {
            IList<FeriadoViewModel> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Feriado> repository = new Repository<Feriado>(context, unitOfWork);
                var service = new FeriadoService(repository);
                lista = service.Query(s => s.Localidade.Id == localidadeId).Select().ToList().OrderByDescending(o => o.Data).Select(s => new FeriadoViewModel { Id = s.Id, CompetenciaId = s.Competencia.Id, LocalidadeId = s.Localidade.Id, Data = s.Data, Descricao = s.Descricao }).ToList();
            }
            return lista;
        }

        public static IList<UsuarioViewModel> GetUsuariosViewModel()
        {
            IList<UsuarioViewModel> lista;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                //Lista de Competencias
                IRepositoryAsync<Usuario> repository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService service = new UsuarioService(repository);
                lista = service.Query().Select().ToList().Select(u => new UsuarioViewModel { Id = u.Id, Nome = u.Nome, Sigla = u.Sigla, Email = u.Email, Situacao = u.Situacao, DataInicio = u.DataInicio, DataTermino = u.DataTermino, LiderId = u.Lider.Id, LoginAssociado = false }).OrderBy(o => o.Nome).ToList();
            }
            return lista;
        }

        public static IList<UsuarioViewModel> GetUsuariosViewModel(List<ApplicationUser> list)
        {
            var usuarios = GetUsuariosViewModel();
            foreach (var usuario in list.Select(appUsers => usuarios.FirstOrDefault(u => u.Email == appUsers.Email)).Where(usuario => usuario != null))
            {
                usuario.LoginAssociado = true;
            }
            return usuarios;
        }

        public static bool IsSupplyCategory(int tipoRelacaoId)
        {
            TipoRelacao tpRelacao;
            var returnValue = false;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                //Lista de Competencias
                IRepositoryAsync<TipoRelacao> repository = new Repository<TipoRelacao>(context, unitOfWork);
                TipoRelacaoService service = new TipoRelacaoService(repository);
                tpRelacao = service.Query(t => t.Id == tipoRelacaoId).Select().OrderBy(o => o.Descricao).FirstOrDefault();
                if (tpRelacao == null) return false;
                var supplyRelantionship = Properties.Settings.Default.SupplyRelantionship;
                if (!string.IsNullOrEmpty(supplyRelantionship) || !string.IsNullOrWhiteSpace(supplyRelantionship))
                {
                    var tiposRelacaoConfiguration = supplyRelantionship.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue = tiposRelacaoConfiguration.Any(t => string.Equals(t.Trim(), tpRelacao.Descricao.Trim(), StringComparison.CurrentCultureIgnoreCase));
                }

            }
            return returnValue;
        }

    }
}