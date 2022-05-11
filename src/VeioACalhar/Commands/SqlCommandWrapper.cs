using System.Data;
using System.Data.SqlClient;

namespace VeioACalhar.Commands;

public class SqlCommandWrapper : ISqlCommand
{
    private readonly SqlCommand command;
    private readonly SqlConnection connection;

    public SqlCommandWrapper(string query, SqlConnection connection)
    {
        command = new SqlCommand(query, connection);
        this.connection = connection;
    }

    public int ExecuteNonQuery()
    {
        connection.Open();
        return command.ExecuteNonQuery();
    }

    public SqlDataReader ExecuteReader()
    {
        connection.Open();
        return command.ExecuteReader();
    }

    public object? ExecuteScalar()
    {
        connection.Open();
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
}
