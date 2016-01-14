using System;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ContC.crosscutting.Redis
{
    public class RedisOperacao : IRedisOperacao
    {
        public ConnectionMultiplexer redis = RedisConexao.connection;

        public T Salvar<T>(string chave, T t) where T: class
        {
            return Salvar(chave, t, 0);
        }

        public T Salvar<T>(string chave, T t, TimeSpan expirarEm) where T : class
        {
            int segundos = expirarEm.Seconds;
            return Salvar(chave, t, segundos);
        }

        public T Salvar<T>(string chave, T t, int segundosExpirar) where T : class
        {
            TimeSpan? expiracao = null;

            if (segundosExpirar > 0) {
                expiracao = new TimeSpan(0, 0, segundosExpirar);
            }
            string json = JsonConvert.SerializeObject(t);
            IDatabase database = GetDatabase();
            if (database.StringSet(chave, json, expiracao))
            {
                return t;
            }

            return null;
        }

        public T Get<T>(string chave) where T : class
        {
            IDatabase database = GetDatabase();
            string json = database.StringGet(chave);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Excluir(string chave)
        {
            IDatabase database = GetDatabase();
            database.KeyDelete(chave);
        }

        private IDatabase GetDatabase()
        {
            return redis.GetDatabase();
        }

      
    }
}