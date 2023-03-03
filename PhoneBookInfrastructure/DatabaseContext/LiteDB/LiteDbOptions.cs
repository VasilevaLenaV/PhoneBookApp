using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure.DatabaseContext
{
    public class LiteDbOptions
    {
        public string? DatabaseLocation { get; internal set; }
        public SqlDbTypes SqlDbType { get; set; }
    }

    public enum SqlDbTypes
    {
        LiteDB = 1
    }
}
