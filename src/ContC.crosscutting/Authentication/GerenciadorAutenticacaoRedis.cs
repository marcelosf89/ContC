using System;
using ContC.crosscutting.Authentication.Interface;
using ContC.crosscutting.DataContracts;
using ContC.crosscutting.Redis;

namespace ContC.crosscutting.Authentication
{
    public class GerenciadorAutenticacaoRedis : IGerenciadorAutenticacao
    {

        public GerenciadorAutenticacaoRedis(IRedisOperacao redisOperacao)
        {
            _redisOperacao = redisOperacao;
        }

        public UsuarioSessao Get()
        {
            string userId = UserIdentity.RedisValue;
            UsuarioSessao retorno = _redisOperacao.Get<UsuarioSessao>(userId);
            if (retorno == null)
            {
                return new UsuarioSessao();
            }

            return retorno;
        }

        public void Registrar(UsuarioSessao usuario)
        {
            UserIdentity.SetRedisValue();
            _redisOperacao.Salvar(UserIdentity.RedisValue, usuario, new TimeSpan(1, 0, 0, 0));
        }

        public void Logoff()
        {
            _redisOperacao.Excluir(UserIdentity.RedisValue);
        }

        private IRedisOperacao _redisOperacao;

    }
}
