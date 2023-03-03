using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure.DatabaseContext
{
    public class LiteDbContext : ILiteDbContext,IDisposable
    {
        public LiteDatabase Database { get; }

        public LiteDbContext()
        {
            Database = new LiteDatabase("contacts.db");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }


}
