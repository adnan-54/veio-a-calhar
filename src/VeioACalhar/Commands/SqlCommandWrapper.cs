using System.Data;
using System.Data.SqlClient;

namespace VeioACalhar.Commands;

public class SqlCommandWrapper : ISqlCommand
{
    private readonly SqlConnection connection;
    private readonly SqlCommand command;

    public SqlCommandWrapper(string query, SqlConnection connection)
    {
        this.connection = connection;
        command = new(query, connection);
    }

    public int ExecuteNonQuery()
    {
        TryOpenConnection();
        return command.ExecuteNonQuery();
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

    public void AddParameter(string parameterName, object? value)
    {
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
