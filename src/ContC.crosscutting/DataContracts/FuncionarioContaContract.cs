using System;
using System.ComponentModel.DataAnnotations;

namespace ContC.crosscutting.DataContracts
{
    public class FuncionarioContaContract
    {

        public virtual int Id { get; set; }

        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        public virtual string Nome { get; set; }

        public virtual string Identificacao1 { get; set; }

        public virtual string Identificacao2 { get; set; }

        [Required(ErrorMessage = "A data de Nascimento é obrigatório")]
        public virtual DateTime? Nascimento { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório")]
        public virtual string Telefone { get; set; }

        public virtual int LiderId { get; set; }

        [Required(ErrorMessage = "O Tipo de pagamento é obrigatório")]
        public virtual int TipoPagamentoId { get; set; }

        [Required(ErrorMessage = "O Tipo de Regime é obrigatório")]
        public virtual int TipoRegimeFuncionarioId { get; set; }

        [Required(ErrorMessage = "O Valor de pagamento é obrigatório")]
        public virtual decimal Valor { get; set; }

        public virtual string Sigla { get; set; }

        public virtual bool Situacao { get; set; }

        public int BancoId { get; set; }

        public string Agencia { get; set; }

        public string Conta { get; set; }

        public string Digito { get; set; }

        public String Erro { get; set; }

        public int ContaId { get; set; }
    }
}
