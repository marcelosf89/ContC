using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContC.presentation.mvc.Models
{
    public class MensagemViewModel
    {
        /// <summary>
        /// Define os tipos de mensagens que serão exibidos para o usuário
        /// null - INFORMAÇÃO,
        /// 0 - SUCESSO
        /// 1 - ERRO
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Define o texto que será exibido para o usuário
        /// </summary>
        public string Texto { get; set; }
    }
}