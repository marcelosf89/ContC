using ContC.crosscutting.Authentication.Interface;
using ContC.crosscutting.DataContracts;
using System.Web;
using Newtonsoft.Json;
using System;

namespace ContC.crosscutting.Authentication
{
    public class GerenciadorAutenticacaoCookie : IGerenciadorAutenticacao
    {
        public UsuarioSessao Get()
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpCookie cookie = request.Cookies.Get("contc");
            if (cookie != null && !(string.IsNullOrEmpty(cookie.Value)))
            {
                return JsonConvert.DeserializeObject<UsuarioSessao>(cookie.Value);
            }

            return new UsuarioSessao();
        }

        public void Registrar(UsuarioSessao usuario)
        {
            if (usuario != null && usuario.Autenticado)
            {
                HttpResponse response = HttpContext.Current.Response;
                HttpCookie cookie = new HttpCookie("contc");
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = JsonConvert.SerializeObject(usuario);
                response.Cookies.Add(cookie);
            }
        }

    }
}
