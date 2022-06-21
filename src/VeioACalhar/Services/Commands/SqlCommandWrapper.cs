using System.Data;
using System.Data.SqlClient;

namespace VeioACalhar.Commands;

public sealed class SqlCommandWrapper : ISqlCommand
{
    private readonly SqlConnection connection;
    private readonly SqlCommand command;

    public SqlCommandWrapper(string query, SqlConnection connection)
    {
        this.connection = connection;
        command = new(query, connection);
    }

    public void ExecuteNonQuery()
    {
        TryOpenConnection();
        command.ExecuteNonQuery();
    }

    public SqlDataReader ExecuteReader()
    {
        TryOpenConnection();
        return command.ExecuteReader();
    }

    public object? ExecuteScalar()
    {
        TryOpenConnection();
        return command.ExecuteScalar();
    }

    public T? ExecuteScalar<T>()
    {
        try
        {
            var result = ExecuteScalar();
            if (result is null)
                return default;
            return (T)result;
        }
        catch
        {
            return default;
        }
    }

    public void AddParameter(string parameterName, object? value)
    {
        if (value is null || (value is string str && string.IsNullOrWhiteSpace(str)))
            value = DBNull.Value;

        command.Parameters.AddWithValue(parameterName, value);
    }

    public void Dispose()
    {
        command.Dispose();
        if (connection.State != ConnectionState.Closed)
            connection.Close();
    }

    private void TryOpenConnection()
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();
    }
}
