using Business;

namespace InternalApi
{
    public class BasicConfiguration : BaseConfiguration
    {
        public string ConnectionString { get; private set; }
        public string Key { get; private set; }
        public string Issuer { get; private set; }

        public BasicConfiguration(IConfiguration configuration) : base(configuration) { }
    }
}