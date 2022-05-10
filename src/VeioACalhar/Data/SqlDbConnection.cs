using System.Data.SqlClient;

namespace VeioACalhar.Data;

public class SqlDbConnection : IDbConnection
{
    private readonly ILogger<SqlDbConnection> logger;
    private readonly SqlConnection connection;

    public SqlDbConnection(SqlConnection connection, ILogger<SqlDbConnection> logger)
    {
        this.connection = connection;
        this.logger = logger;
    }

    public void ExecuteNonQuery(SqlCommand command)
    {
        try
        {
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            logger.Log(LogLevel.Error, ex, "An error occurred while executing the command");
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public SqlDataReader ExecuteReader(SqlCommand command)
    {
        try
        {
            connection.Open();
            command.Connection = connection;
            return command.ExecuteReader();
        }
        catch (Exception ex)
        {
            logger.Log(LogLevel.Error, ex, "An error occurred while executing the command");
            throw;
        }
        finally
        {
            connection.Close();
        }
    }
}
