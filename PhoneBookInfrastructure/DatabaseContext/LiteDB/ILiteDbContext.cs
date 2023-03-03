using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure
{
    public interface ILiteDbContext
    {
        public LiteDatabase Database { get; }
    }
}
