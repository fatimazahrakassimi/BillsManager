using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManager
{
    public interface  IDatabaseConnection
    {
        //pour établir la conexion a la BD sqlite
        SQLite.SQLiteConnection DbConnection();
    }
    //par la suite on va créer des classes sur chaque projet qui implementeront cette interface 
}
