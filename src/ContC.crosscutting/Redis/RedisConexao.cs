using StackExchange.Redis;

namespace ContC.crosscutting.Redis
{
    public class RedisConexao
    {
        public static ConnectionMultiplexer connection
        {
            get
            {
                return redis;
            }
        }


        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    }
}
