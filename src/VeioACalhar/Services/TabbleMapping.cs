using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using VeioACalhar.Models;

namespace VeioACalhar.Services;

public class TabbleMapping<TModel> where TModel : Entidade
{
    private readonly Dictionary<string, Func<TModel, object?>> mappings;

    public TabbleMapping()
    {
        mappings = new();
        ModelType = typeof(TModel);
        TableName = ModelType.GetCustomAttribute<TableAttribute>()?.Name ?? throw new Exception();
        CreateMappings();
    }

    public Type ModelType { get; }

    public string TableName { get; }

    private void CreateMappings()
    {
        var properties = ModelType.GetProperties();

        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<ColumnAttribute>();
            if (attribute is null)
                continue;

            mappings.Add(attribute.Name!, model => property.GetValue(model));
        }
    }
}