using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class TelefoneRepository : ITelefoneRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public TelefoneRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public IEnumerable<Telefone> CreateFor(Pessoa pessoa)
    {
        foreach (var telefone in pessoa.Telefones)
            yield return Create(telefone, pessoa);
    }

    public IEnumerable<Telefone> GetFor(Pessoa pessoa)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Telefone> UpdateFor(Pessoa pessoa)
    {
        throw new NotImplementedException();
    }

    public void DeleteFor(Pessoa pessoa)
    {
        throw new NotImplementedException();
    }

    private Telefone Create(Telefone telefone, Pessoa pessoa)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas_Telefones(Id_Pessoa, Numero) OUTPUT INSERTED.Id VALUES (@Id_Pessoa, @Numero)");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.AddParameter("@Numero", telefone.Numero);

        var id = command.ExecuteNonQuery();

        return telefone with { Id = id, Pessoa = pessoa };
    }
}
