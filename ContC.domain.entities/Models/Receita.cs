using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Receita : Entidade
    {
        //TODO Solucao para problema de conversao datetime/datetime2 do Entity: DateTime?
        //http://stackoverflow.com/questions/18795762/entity-framework-cannot-save-conversion-of-datetime2-to-datetime-failing

        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime DataRecebimento { get; set; }
        public virtual Grupo Endereco { get; set; }
        public virtual TipoReceita TipoReceita { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
