using Business;

namespace InternalApi
{
    public class BasicConfiguration : BaseConfiguration
    {
        public string ConnectionString { get; private set; }
        public string Key { get; private set; }
        public string Issuer { get; private set; }
        public string SMTPHost { get; private set; }
        public int SMTPPort { get; private set; }
        public bool UseSMTPSsl { get; private set; }
        public string SMTPUsername { get; private set; }
        public string SMTPPassword { get; private set; }

        public BasicConfiguration(IConfiguration configuration) : base(configuration) { }
    }
}