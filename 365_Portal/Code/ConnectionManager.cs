using System.Web.Configuration;

namespace _365_Portal.Code
{
    public class ConnectionManager
    {
        public static string connectionString = WebConfigurationManager.ConnectionStrings["365LifeConnection"].ConnectionString;
    }
}