namespace VeioACalhar.Models;

public class Funcionario : PessoaFisica
{
    public Cargo? Cargo { get; set; }

    public Usuario? Usuario { get; set; }

    public decimal Salario { get; set; }
}