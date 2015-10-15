using Entity.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class NotaItem : Entidade
    {
        public virtual int Id { get; set; }
        public virtual String Titulo { get; set; }
        public virtual DateTime Cadastro { get; set; }
        public virtual DateTime? Concluido { get; set; }
        public virtual Usuario ConcluidoPor { get; set; }
        public virtual Usuario CadastradoPor { get; set; }
        public virtual Nota Lista { get; set; }

        public virtual DateTime? Cancelado { get; set; }
        public virtual Usuario CanceladoPor { get; set; }
    }
}
