using System.Data.SqlClient;

namespace VeioACalhar.Data;

public interface IDbConnection
{
    void ExecuteNonQuery(SqlCommand command);

    SqlDataReader ExecuteReader(SqlCommand command);
}