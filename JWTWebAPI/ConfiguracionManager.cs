namespace JWTWebAPI
{
    static class ConfiguracionManager
    {
        public static IConfiguration AppSetting
        {
            get;
        }
        static ConfiguracionManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
