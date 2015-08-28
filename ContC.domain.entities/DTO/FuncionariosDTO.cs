using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.DTO
{
    public class FuncionariosDTO
    {
        public int EmpresaId { get; set; }
        public IList<FuncionarioLiderDTO> Lideres { get; set; }
        public IList<FuncionarioTipoPagamentoDTO> TipoPagamentos { get; set; }
        public IList<FuncionarioTipoRegimeFuncionarioDTO> TipoRegimeFuncionarios { get; set; }
        
    }

    public class FuncionarioLiderDTO
    {
        public int Id { get; set; }
        public String Nome { get; set; }

    }

    public class FuncionarioTipoPagamentoDTO
    {
        public int Id { get; set; }
        public String Nome { get; set; }
    }

    public class FuncionarioTipoRegimeFuncionarioDTO
    {
        public int Id { get; set; }
        public String Nome { get; set; }
    }
    
}
