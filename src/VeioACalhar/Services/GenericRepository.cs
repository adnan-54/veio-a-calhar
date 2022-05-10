using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Reflection;
using VeioACalhar.Data;
using VeioACalhar.Models;

namespace VeioACalhar.Services;

public abstract class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : Entidade, new()
{
    private readonly IDbConnection dbConnection;
    private readonly Dictionary<string, PropertyInfo> mappings;
    private readonly Type modelType;
    private readonly string tableName;

    protected GenericRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
        mappings = new();
        modelType = typeof(TModel);
        tableName = modelType.GetCustomAttribute<TableAttribute>()!.Name;

        CreateMappings();
    }

    public virtual TModel Create(TModel model)
    {
        var command = new SqlCommand($"INSERT INTO {tableName}({GetCreateColumnsList()}) VALUES ({GetCreateParametersList()})");
        FillCreateParametersValues(command, model);

        var id = dbConnection.ExecuteScalar(command);
        if (id < 0)
            throw new Exception("An error occured while trying to create the model");

        model.Id = id;
        return model;
    }

    public TModel Get(int id)
    {
        var command = new SqlCommand($"SELECT * FROM {tableName} WHERE Id=@Id");
        command.Parameters.AddWithValue("@Id", id);
        var reader = dbConnection.ExecuteReader(command);
        return CreateFromReader(reader);
    }

    public IEnumerable<TModel> Get()
    {
        var command = new SqlCommand($"SELECT * FROM {tableName}");
        var reader = dbConnection.ExecuteReader(command);
        while(reader.Read())
            yield return CreateFromReader(reader);
    }

    public void Update(TModel model)
    {
        if (!model.Id.HasValue)
            throw new Exception("The given model does not have an Id");
        var id = model.Id.Value;

        var command = new SqlCommand($"UPDATE {tableName} SET {CreateUpdateList()} WHERE Id=@Id");
        command.Parameters.AddWithValue("@Id", id);
        FillCreateParametersValues(command, model);
        dbConnection.ExecuteNonQuery(command);
    }

    private object CreateUpdateList()
    {
        var parameters = mappings.Keys.Where(key => !key.Equals("id", StringComparison.OrdinalIgnoreCase)).Select(key => $"{key}=@{key}");
        return string.Join(',', parameters);
    }

    public void Delete(TModel model)
    {
        if(!model.Id.HasValue)
            throw new Exception("The given model does not have an Id");
        var id = model.Id.Value;

        var command = new SqlCommand($"DELETE FROM {tableName} WHERE Id=@Id");
        command.Parameters.AddWithValue("@Id", id);
        dbConnection.ExecuteNonQuery(command);
    }

    private void CreateMappings()
    {
        var properties = modelType.GetProperties();

        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<ColumnAttribute>(true);
            if (attribute is null)
                continue;

            mappings.Add(attribute.Name!, property);
        }
    }

    protected string GetCreateColumnsList()
    {
        var columns = mappings.Keys.Where(k => !k.Equals("id", StringComparison.OrdinalIgnoreCase));
        return $"{string.Join(',', columns)}";
    }

    protected string GetCreateParametersList()
    {
        var parameters = mappings.Keys.Where(key => !key.Equals("id", StringComparison.OrdinalIgnoreCase)).Select(key => $"@{key}");
        return $"{string.Join(',', parameters)}";
    }

    protected void FillCreateParametersValues(SqlCommand command, TModel model)
    {
        foreach (var mapping in mappings)
        {
            var key = mapping.Key;
            if (key.Equals("id", StringComparison.OrdinalIgnoreCase))
                continue;
            var value = mapping.Value.GetValue(model);
            command.Parameters.AddWithValue($"@{key}", value);
        }
    }

    protected TModel CreateFromReader(SqlDataReader reader)
    {
        if (!reader.HasRows)
            throw new Exception("No entities found with the given id");
        if(!reader.Read())
            throw new Exception("An error occurred in the reader");
        var model = new TModel();

        foreach(var mapping in mappings)
        {
            var property = mapping.Value;
            var value = reader[mapping.Key];
            property.SetValue(model, value);
        }

        return model;
    }
}
