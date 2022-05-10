using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace VeioACalhar.Models;

public abstract class Entidade
{
    [Column("Id")]
    public int? Id { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Entidade entidade)
            return false;

        var currentType = GetType();
        var targetType = obj.GetType();

        if (currentType != targetType)
            return false;

        var currentTable = currentType.GetCustomAttribute<TableAttribute>()?.Name;
        var targetTable = targetType.GetCustomAttribute<TableAttribute>()?.Name;

        if (currentTable is null || targetTable is null)
            return false;

        if (!currentTable.Equals(targetTable, StringComparison.InvariantCultureIgnoreCase))
            return false;

        return Id == entidade.Id;
    }

    public override int GetHashCode()
    {
        var hash = base.GetHashCode();
        var properties = GetType().GetProperties();
        foreach (var property in properties)
            hash += property.GetValue(this)?.GetHashCode() * 17 ?? 0;
        return hash;
    }
}
