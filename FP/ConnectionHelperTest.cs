using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using LaYumba.Functional;
using static FP.ConnectionHelperTest;

namespace FP;


public static class ConnectionHelper
{
    public static R Connect<R>
       (string connString, Func<SqlConnection, R> func)
    {
        using var conn = new SqlConnection(connString);
        conn.Open();
        return func(conn);
    }

    public static R Transact<R>
       (SqlConnection conn, Func<SqlTransaction, R> f)
    {
        using var tran = conn.BeginTransaction();

        R r = f(tran);
        tran.Commit();

        return r;
    }
}



public class ConnectionHelperTest
{
    public void Run()
    {

    }
}
