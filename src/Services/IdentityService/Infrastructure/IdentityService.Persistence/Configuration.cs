using Microsoft.Extensions.Configuration;

namespace IdentityService.Persistence
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();

                configurationManager.SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent!.FullName}\\Infrastructure\\IdentityService.Persistence");
                configurationManager.AddJsonFile("PrivateInformations.json");
                return configurationManager.GetConnectionString("MsSQL");
            }
        }
    }
}
