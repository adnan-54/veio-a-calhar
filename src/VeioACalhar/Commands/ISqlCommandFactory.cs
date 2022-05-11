namespace VeioACalhar.Commands;

public interface ISqlCommandFactory
{
    ISqlCommand Create(string query);
}
