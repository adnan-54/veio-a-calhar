using System.Data.SqlClient;

namespace VeioACalhar.Commands;

public interface ISqlCommand : IDisposable
{
    int ExecuteNonQuery();

    SqlDataReader ExecuteReader();

    object? ExecuteScalar();

    void AddParameter(string parameterName, object? value);
}