using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using LaYumba.Functional;
using static FP.ConnectionHelper;
using System.Reflection;

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

public record SqlTemplate(string Value)
{
    public static implicit operator string(SqlTemplate c) => c.Value;
    public static implicit operator SqlTemplate(string s) => new(s);

    public override string ToString() => Value;
}

public static class ConnectionStringExt
{
    public static Func<object, IEnumerable<T>> Retrieve<T>
    (
        this ConnectionString connStr,
        SqlTemplate sql
    )
    => param
    => Connect(connStr, conn => conn.Query<T>(sql, param));
}

public class Employee { public string LastName { get; } }


public class ConnectionHelperTest
{
    public void Run()
    {
        ConnectionString conn = "localhost";

        SqlTemplate sel = "SELECT * FROM EMPLOYEES"
            , sqlById = $"{sel} WHERE ID = @Id"
            , sqlByName = $"{sel} WHERE LASTNAME = @LastName";

        // queryById : object -> IEnumerable<Employee>
        var queryById = conn.Retrieve<Employee>(sqlById);

        // queryByLastName : object -> IEnumerable<Employee>
        var queryByLastName = conn.Retrieve<Employee>(sqlByName);

        // lookupEmployee: Guid -> Option<Employee>
        Option<Employee> lookupEmployee(Guid id)
            => queryById(new { Id = id }).SingleOrDefault();

        // findEmloyeesByLastName : string -> IEnumerable<Employee>
        IEnumerable<Employee> findEmployeesByLastName(string lastName)
            => queryByLastName(new { LastName = lastName });

        Console.ReadLine();
    }
}
