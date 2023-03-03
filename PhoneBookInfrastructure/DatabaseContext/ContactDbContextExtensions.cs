using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure.DatabaseContext
{
     public static class ContactDbContextExtensions
    {
        public static IServiceCollection ContactDbContext(this IServiceCollection serviceCollection, LiteDbOptions liteDbOption)
        {
            serviceCollection.AddSingleton(liteDbOption);
            switch (liteDbOption.SqlDbType)
            {
                case SqlDbTypes.LiteDB:
                    serviceCollection.AddSingleton<ILiteDbContext, LiteDbContext>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return serviceCollection;
        }
    }
}
