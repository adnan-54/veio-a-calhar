namespace VeioACalhar.Models;

public abstract class PessoaFisica : Pessoa
{
    public string? CPF  { get; init; }
    
    public string? RG  { get; init; }
}
