using System.Data.SqlClient;

namespace VeioACalhar.Data;

public interface IDbConnection
{
    int ExecuteScalar(SqlCommand command);

    void ExecuteNonQuery(SqlCommand command);

    SqlDataReader ExecuteReader(SqlCommand command);
}