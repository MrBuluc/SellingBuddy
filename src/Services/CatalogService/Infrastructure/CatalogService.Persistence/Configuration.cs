using Microsoft.Extensions.Configuration;

namespace CatalogService.Persistence
{
    public class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\Infrastructure\\CatalogService.Persistence");
                configurationManager.AddJsonFile("PrivateInformations.json");
                return configurationManager.GetConnectionString("MsSQL");
            }
        }
    }
}
