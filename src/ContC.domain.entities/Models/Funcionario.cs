using System;
using Entity.Pattern;
using System.Collections.Generic;

namespace ContC.domain.entities.Models
{
    public class Funcionario : Entidade
    {
        public Funcionario()
        {
        }

        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Email { get; set; }

        public virtual string Telefone { get; set; }

        public virtual DateTime? DataInicio { get; set; }

        public virtual DateTime? DataTermino { get; set; }

        public virtual Funcionario Lider { get; set; }

        public virtual TipoPagamento TipoPagamento { get; set; }

        public virtual TipoRegimeFuncionario TipoRegimeFuncionario { get; set; }

        public virtual decimal Valor { get; set; }

        public virtual string Identificacao2 { get; set; }

        public virtual string Identificacao1 { get; set; }

        public virtual DateTime Nascimento { get; set; }

        public virtual IList<Empresa> Empresas { get; set; }

        public virtual void AdicionarEmpresa(Empresa empresa)
        {
            if (Empresas == null)
            {
                Empresas = new List<Empresa>();
            }

            Empresas.Add(empresa);
        }

    }
}
