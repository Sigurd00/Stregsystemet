using System;
using System.Data;
using System.Data.SqlClient;

public class Database : IDisposable
{
    private readonly string _connectionString;
    private SqlConnection _connection;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
        _connection = new SqlConnection(_connectionString);
        _connection.Open();
    }

    public IDbConnection GetOpenConnection()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
