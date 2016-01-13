using System.Collections.Generic;

namespace ContC.crosscutting
{
    public class OperacaoStatus
    {
        public bool Ok {
            get{
                return _erros != null && _erros.Count > 0;
            }
        }

        public IList<string> Erros {
            get
            {
                return new List<string>(_erros);
            }
        }
        
        public void AdicionarErro(string erro)
        {
            if (!string.IsNullOrEmpty(erro))
            {
                _erros.Add(erro);
            }
        }

        private IList<string> _erros =  new List<string>();
    }
}
