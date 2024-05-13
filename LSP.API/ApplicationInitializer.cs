
using LSP.Dal.Concrete.Context;
using Microsoft.EntityFrameworkCore;

namespace LSP.API
{
    public class ApplicationInitializer
    {
        public void Initialize(WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = new LSPDbContext();
                context.Database.Migrate();

            }
            catch (Exception e)
            {
                application.Logger.LogError(e, "An error occurred while initializing the application.");
                throw;
            }
        }
    }
}