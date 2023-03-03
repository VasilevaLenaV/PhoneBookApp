using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBookApp.Services;
using PhoneBookDomain.Repository;
using PhoneBookInfrastructure;
using PhoneBookInfrastructure.DatabaseContext;

public class Program
{
     private static void Main(string[] args)
     {
          Console.Clear();
          Console.WriteLine("Hello, PhoneBookApp!");

          var services = new ServiceCollection();
          ConfigureServices(services);

          services
              .AddSingleton<PhoneBookAppClient, PhoneBookAppClient>()
              .AddSingleton<ILiteDbContext, LiteDbContext>();

          services.BuildServiceProvider()?.GetService<PhoneBookAppClient>()?.Start();
     }

     private static void ConfigureServices(IServiceCollection services)
     {
          services
              .AddSingleton<IContactMenuService, ContactMenuService>()
              .AddSingleton<IContactActionService, ContactActionService>()
              .AddScoped<IContactRepository, ContactRepository>();
     }
}