using System;
using ContC.crosscutting.Authentication.Interface;
using ContC.crosscutting.DataContracts;
using System.Web;
using Newtonsoft.Json;

namespace ContC.crosscutting.Authentication
{
    public class GerenciadorAutenticacaoCookie : IGerenciadorAutenticacao
    {
        public UsuarioSessao Get()
        {
            HttpResponse response = HttpContext.Current.Response;
            HttpCookie cookie = response.Cookies["contc"];
            if (cookie != null)
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
                cookie.Value = JsonConvert.SerializeObject(usuario);
                response.Cookies.Add(cookie);
            }
        }

    }
}
