using System;

namespace ContC.crosscutting.Redis
{
    public interface IRedisOperacao
    {
        void Excluir(string chave);
        T Get<T>(string chave) where T : class;
        T Salvar<T>(string chave, T t) where T : class;
        T Salvar<T>(string chave, T t, int segundosExpirar) where T : class;
        T Salvar<T>(string chave, T t, TimeSpan expirarEm) where T : class;
    }
}